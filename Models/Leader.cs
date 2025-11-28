using Models;
namespace TestProject6.BYT_Pokemon.Models;

[Serializable]
public class Leader : Trainer
{
    private static List<Leader> _extent = new List<Leader>();
    private string _specialPrefix;
    //public Team? Team { get; set; }

    public string SpecialPrefix
    {
        get => _specialPrefix;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Special prefix can not be empty.");
            _specialPrefix = value;
        }
    }
    
    //TODO:constructor
    //TODO:save/load

    public Leader()
    {
        AddLeader(this);
    }

    public Leader(int totalMoney, Badge[] badges, string? status, string name, int age, string specialPrefix)
    {
        TotalMoney = totalMoney;
        Badges = badges;
        Status = status;
        Name = name;
        Age = age;
        SpecialPrefix = specialPrefix;
        AddLeader(this);
    }

    private static void AddLeader(Leader leader)
    {
        if (leader == null)
            throw new ArgumentException("Leader cannot be null.");
        _extent.Add(leader);
    }

    public static List<Leader> GetLeaders()
    {
        return _extent;
    }

    public new static void Save(string path = "leaders.xml")
    {
        Serializer.Save(path, _extent);
    }

    public new static bool Load(string path = "leaders.xml")
    {
        return Serializer.Load(path, _extent);
    }
}