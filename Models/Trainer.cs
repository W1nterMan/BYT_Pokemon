using Models;

namespace Models;

[Serializable]
public class Trainer : Person
{
    public int TrainerId { get; }
    private int _totalMoney;
    private string[] _badges = Array.Empty<string>();
    private string _status;
    public Team? Team { get; internal set; }

    public string[] Badges
    {
        get => _badges;
        set
        {
            if (value == null) throw new ArgumentNullException(nameof(Badges));
            if (value.Any(b => string.IsNullOrWhiteSpace(b))) throw new ArgumentException("Badge can not be empty or null.");
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

    public string Status
    {
        get => _status;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || !Enum.IsDefined(typeof(TrainerStatus),value))
            {
                throw new ArgumentException("Invalid status.");
            }
            _status = value;
        }
    }
    
    public Trainer() { }

    public Trainer(int trainerId, int totalMoney, string[] badges, string? status, string name, int age) : base(name,age)
    {
        TrainerId = trainerId;
        TotalMoney = totalMoney;
        Badges = badges;
        Status = status;
    }

    // public void Move() {}
    // public void ChangeActivePokemon() {}
    // public void ChallengeTrainer(Trainer opponent) {}
    // public void SwitchAvailability() {}
    // public void Retire() {}
}

public enum TrainerStatus
{
    Active,
    Retired
}
