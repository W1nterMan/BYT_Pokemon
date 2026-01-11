using System.Xml.Serialization;

namespace Models;

[Serializable]
public class Leader
{
    private Person _person;

    [XmlIgnore]
    public Person Person => _person;

    private string _specialPrefix;

    public string SpecialPrefix
    {
        get => _specialPrefix;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Special prefix can not be empty or whitespaces.");
            _specialPrefix = value;
        }
    }
    
    private Team _team;
    
    [XmlIgnore]
    public Team Team
    {
        get => _team;
        set => _team = value;
    }

    public Leader() { }

    public Leader(Person person, string specialPrefix, Team team)
    {
        _person = person ?? throw new ArgumentNullException(nameof(person));
        SpecialPrefix = specialPrefix;
        Team = team;
        team.Leader = this;
    }
}