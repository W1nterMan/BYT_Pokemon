using System.Xml.Serialization;
using Models;

namespace Models;

[Serializable]
public class Trainer : Person
{
    //Attributes
    private string[] _badges = Array.Empty<string>();
    private int _totalMoney;
    private string _status;
    
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
    
    //Associations
    private int _trainerId;
    private Team? _team;

    public int TrainerId
    {
        get => _trainerId;
        set
        {
            if (TrainerId < 0)
            {
                throw new ArgumentException("TrainerId cannot be less than zero.");
            }

            _trainerId = value;
        }
    }
    
    [XmlIgnore]
    public Team? Team
    {
        get => _team;
        set
        {
            _team = value;
        }
    }
    
    // trainer - trainer
    private HashSet<Battle> _battles;

    public HashSet<Battle> Battles
    {
        get => _battles;
    }

    public void RemoveBattle(Battle battle)
    {
        _battles.Remove(battle);
    }

    // trainer - bag
    private Bag _bag;
    
    [XmlIgnore]
    public Bag Bag
    {
        get => _bag;
        set => _bag = value;
    }

    //trainer - leader subset
    [XmlIgnore]
    private HashSet<Leader> _leaders = new HashSet<Leader>();

    public void AddLeaderToSet(Leader leader)
    {
        if (leader == null) throw new ArgumentNullException("Leader cannot be null");
        _leaders.Add(leader);
    }

    public void AddBag()
    {
        if (_bag != null) throw new InvalidOperationException("This Pokecenter already has a PC");

        new Bag(this); 
    }
    
    public Trainer() { }

    public Trainer(int trainerId, int totalMoney, string[] badges, string? status, string name, int age) : base(name,age)
    {
        TrainerId = trainerId;
        TotalMoney = totalMoney;
        Badges = badges;
        Status = status;
        AddBag();
        _battles = new HashSet<Battle>();
    }

    public void AddBattle(Battle battle)
    {
        _battles.Add(battle);
    }
    
    public void DeleteTrainer()
    {
        if (_bag != null)
        {
            Bag.RemoveFromExtent(_bag);
        }

        if (_team == null)
        {
            Team.RemoveTeamMember(this.TrainerId);
        }

        //IDK how to implement, need to ask teacher.
        /*foreach (var battle in _battles)
        {
            if (battle.Trainer1 == this)
            {
                battle.Trainer1 = null;
            }
        }*/

        // if (_leaders.Contains(this))

        RemoveFromExtent(this);
    }

    /*public void ChallengeTrainer(Trainer opponent)
    {
        if (opponent == null)
            throw new ArgumentNullException(nameof(opponent));
        if (opponent == this)
            throw new InvalidOperationException("Trainer cannot compete with himself.");
        if (_challengeTrainer != null || opponent._challengeTrainer != null)
            throw new InvalidOperationException("One of trainers is already competing.");

        _challengeTrainer = opponent;
        opponent._challengeTrainer = this;
    }

    public void StopChallengeTrainer()
    {
        if (_challengeTrainer == null)
            return;

        var tmp = _challengeTrainer;
        _challengeTrainer = null;
        tmp._challengeTrainer = null;
    }*/

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
