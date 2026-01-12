using System.Xml.Serialization;
using Models;

namespace Models;

[Serializable]
public class Team
{
    private static List<Team> _extent = new List<Team>();

    //Attributes
    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Team name cannot be empty.");
            _name = value;
        }
    }

    //Associations
    private Dictionary<int, Trainer> _trainers = new();
    
    private Leader _leader;
    [XmlIgnore]
    public Leader Leader
    {
        get => _leader;
        set
        {
            if (_leader != null) throw new InvalidOperationException("This team already has leader");
            _leader = value ?? throw new ArgumentNullException(nameof(value));
        }
    }

    public void AddLeader(string name, int age, string prefix)
    {
        if (_leader != null) throw new InvalidOperationException("This team already has leader");
        var person = new PersonBuilder(name, age).AsLeader(prefix, this).Build();
        _leader = person.Leader;
    }
    
    public static List<Team> GetTeams()
    {
        return new List<Team>(_extent);
    }

    public IReadOnlyDictionary<int, Trainer> GetTeamMembers()
    {
        return new Dictionary<int, Trainer>(_trainers);
    }

    public void AddTeamMember(Trainer trainer)
    {
        if (trainer == null)
            throw new ArgumentNullException(nameof(trainer));

        if (trainer.Team != null)
            throw new InvalidOperationException("Trainer already belongs to a team.");

        if (_trainers.ContainsKey(trainer.TrainerId))
            throw new InvalidOperationException("Trainer already in this team.");

        _trainers.Add(trainer.TrainerId, trainer);
        trainer.Team = this;
    }

    public void RemoveTeamMember(int trainerId)
    {
        if (!_trainers.TryGetValue(trainerId, out var trainer))
            throw new InvalidOperationException("Trainer not found in this team.");

        _trainers.Remove(trainerId);
        trainer.Team = null;
    }

    public void DeleteTeam()
    {
        foreach (var trainer in _trainers.Values.ToList())
        {
            trainer.Team = null;
        }
        
        if (_leader != null)
        {
            //I dont know whether we want to delete person behind leader too, so for now, only leader is deleted
            _leader.Team = null; //WHY NULL ALLOWED, whatever
            _leader.Person.RemoveLeaderConnection();
            Leader.RemoveFromExtent(_leader);
            _leader = null;
        }
        
        _trainers.Clear();
        _extent.Remove(this);
    }

    
    public Team() { }

    public Team(string name, string trainerName, int age, string prefix)
    {
        Name = name;
        _extent.Add(this);
        AddLeader(trainerName, age, prefix);
        
    }
    
    public Team(string name, Person person, string prefix)
    {
        Name = name;
        person.AddLeaderConnection(prefix,this);
        _leader = person.Leader;
        _extent.Add(this);
    }

    public static void Save(string path = "teams.xml")
    {
        Serializer.Save(path, _extent);
    }

    public static bool Load(string path = "teams.xml")
    {
        var loaded = Serializer.Load(path, _extent);
        if (loaded != null)
        {
            _extent = loaded;
            return true;
        }
        return false;
    }
}