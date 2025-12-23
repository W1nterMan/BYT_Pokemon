using System.Xml.Serialization;
using Models;

namespace Models;

[Serializable]
public class Battle
{
    private static List<Battle> _extent = new List<Battle>();
    private string _status;
    private int _battleXp;
    private int _moneyIncome;
    private DateTime _time;

    public DateTime Time
    {
        get => _time;
        set
        {
            if (value < DateTime.Now)
            {
                throw new ArgumentException("Time cannot be in the past");
            }
            _time = value;
        }
    }
    
    public string Status
    {
        get => _status;
        set
        {
            if (string.IsNullOrEmpty(value) || !Enum.IsDefined(typeof(BattleStatus),value))
            {
                throw new ArgumentException("Invalid status value.");
            }
            _status = value;
        }
    }

    public int BattleXp
    {
        get => _battleXp;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(BattleXp), "XP can not be negative.");
            _battleXp = value;
        }
    }

    public int MoneyIncome
    {
        get => _moneyIncome;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(MoneyIncome), "Money income can not be negative.");
            _moneyIncome = value;
        }
    }

    [XmlIgnore]
    public Trainer? Winner { get; set; }

    //Associations
    
    private Trainer _trainer1;

    [XmlIgnore]
    public Trainer Trainer1
    {
        get => _trainer1;
        set
        {
            if (value == null) throw new ArgumentNullException("Trainer cannot be null");
            _trainer1 = value;
        }
    }
    
    private Trainer _trainer2;

    [XmlIgnore]
    public Trainer Trainer2
    {
        get => _trainer2;
        set
        {
            if (value == null) throw new ArgumentNullException("Trainer cannot be null");
            _trainer2 = value;
        }
    }

    public Battle() { }
    
    public Battle(string status, int battleXp, int moneyIncome, DateTime time, Trainer? winner, Trainer trainer1, Trainer trainer2)
    {
        
        if (trainer1.Battles.Any(b => b.Status == "Ongoing") || trainer2.Battles.Any(b => b.Status == "Ongoing"))
        {
            throw new InvalidOperationException("Trainer is already in an ongoing battle.");
        }
        
        Status = status;
        BattleXp = battleXp;
        MoneyIncome = moneyIncome;
        Time = time;
        Winner = winner;

        Trainer1 = trainer1;
        Trainer2 = trainer2;
        Trainer1.AddBattle(this);
        Trainer2.AddBattle(this);
        
        AddBattle(this);
    }
    
    public void RemoveBattle()
    {
        _trainer1?.RemoveBattle(this);
        _trainer2?.RemoveBattle(this);

        _extent.Remove(this);
    }

    private static void AddBattle(Battle battle)
    {
        if (battle == null)
            throw new ArgumentException("Battle cannot be null.");
        _extent.Add(battle);
    }
    
    public static List<Battle> GetBattles()
    {
        return new List<Battle>(_extent);;
    }

    public static void Save(string path = "battles.xml")
    {
        Serializer.Save(path, _extent);
    }

    public static bool Load(string path = "battles.xml")
    {
        var loadedList = Serializer.Load(path, _extent);
        
        if (loadedList != null)
        {
            _extent = loadedList;
            return true;
        }
        return false;
    }
    
}


public enum BattleStatus
{
    Planned,
    Ongoing,
    Finished
}