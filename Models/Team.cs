using Models;

namespace Models;

[Serializable]
public class Team
{
    private static List<Team> _extent = new List<Team>();
    private string _name = string.Empty;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Team can not be empty.");
            _name = value;
        }
    }
    
    public Team() { }

    public Team(string name)
    {
        Name = name;
        AddTeam(this);
    }

    private static void AddTeam(Team team)
    {
        if (team == null)
        {
            throw new ArgumentException("Team cannot be null.");
        }
        _extent.Add(team);
    }

    public static List<Team> GetTeams()
    {
        return new List<Team>(_extent);
    }

    public static void Save(string path = "teams.xml")
    {
        Serializer.Save(path, _extent);
    }

    public static bool Load(string path = "teams.xml")
    {
        var loadedList = Serializer.Load(path, _extent);
        
        if (loadedList != null)
        {
            _extent = loadedList;
            return true;
        }
        return false;
    }
    
    //public void AddTeamMember(Trainer trainer) { }
}