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

    public Trainer? Winner { get; set; }

    public Battle() { }
    
    public Battle(string status, int battleXp, int moneyIncome, DateTime time, Trainer? winner)
    {
        Status = status;
        BattleXp = battleXp;
        MoneyIncome = moneyIncome;
        Time = time;
        Winner = winner;
        AddBattle(this);
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
        return Serializer.Load(path, _extent);
    }
    
}


public enum BattleStatus
{
    Planned,
    Ongoing,
    Finished
}