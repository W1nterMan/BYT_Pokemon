using Models;
using TestProject3.Models;


namespace TestProject6.BYT_Pokemon.Models;

[Serializable]
public class Battle
{
    //TODO:enum
    private static List<Battle> _extent = new List<Battle>();
    private string _status = "Ongoing";
    private int _battleXp;
    private int _moneyIncome;

    public string? Status
    {
        get => _status;
        set
        {
            if (!string.IsNullOrEmpty(value) &&
                !(value.Equals(nameof(BattleStatus.Ongoing)) || value.Equals(nameof(BattleStatus.Finished))))
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

    public Battle()
    {
        AddBattle(this);
    }
    
    public Battle(string? status, int battleXp, int moneyIncome, Trainer? winner)
    {
        Status = status;
        BattleXp = battleXp;
        MoneyIncome = moneyIncome;
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
        return _extent;
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
    Ongoing,
    Finished
}