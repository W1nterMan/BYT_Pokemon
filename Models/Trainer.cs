using Models;
using TestProject3.Models;

namespace TestProject6.BYT_Pokemon.Models;


[Serializable]
public class Trainer : Person
{
    private static List<Trainer> _extent = new List<Trainer>();
    private int _totalMoney;
    private Badge[] _badges = Array.Empty<Badge>();
    private string? _status;

    public Badge[] Badges
    {
        get => _badges;
        set
        {
            if (value == null) throw new ArgumentNullException(nameof(Badges));
            if (value.Any(b => b == null)) throw new ArgumentException("Badge can not be null.");
            _badges = value;
        }
    }

    public int TotalMoney
    {
        get => _totalMoney;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(TotalMoney), "Money cannot be negative.");
            _totalMoney = value;
        }
    }

    public string? Status
    {
        get => _status;
        set
        {
            if (!string.IsNullOrEmpty(value) &&
                !(value.Equals(nameof(TrainerStatus.Active)) || value.Equals(nameof(TrainerStatus.Retired))))
            {
                throw new ArgumentException("Status must be Active or Defeated.");
            }
            _status = value;
        }
    }

    //TODO: redo
    public Trainer()
    {
        AddTrainer(this);
    }

    public Trainer(int totalMoney, Badge[] badges, string? status, string name, int age)
    {
        TotalMoney = totalMoney;
        Badges = badges;
        Status = status;
        Name = name;
        Age = age;
        AddTrainer(this);
    }
    
    private static void AddTrainer(Trainer trainer)
    {
        if (trainer == null)
        {
            throw new ArgumentException("Trainer cannot be null.");
        }
        _extent.Add(trainer);
    }

    public static List<Trainer> GetTrainers()
    {
        return _extent;
    }

    public new static void Save(string path = "trainers.xml")
    {
        Serializer.Save(path, _extent);
    }

    public new static bool Load(string path = "trainers.xml")
    {
        return Serializer.Load(path, _extent);
    }

    public void Move() {}
    public void ChangeActivePokemon() {}
    public void ChallengeTrainer(Trainer opponent) {}
    public void SwitchAvailability() {}
    public void Retire() {}
}

public enum TrainerStatus
{
    Active,
    Retired
}
