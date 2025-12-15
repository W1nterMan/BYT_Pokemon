using Models;

namespace Models;

[Serializable]
public class Team
{
    private static List<Team> _extent = new();

    private string _name = string.Empty;

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

    private Dictionary<int, Trainer> _trainers = new();

    public Team() { }

    public Team(string name)
    {
        Name = name;
        _extent.Add(this);
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