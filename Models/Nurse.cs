using System.Xml.Serialization;
using Models;

namespace Models;

[Serializable]
public class Nurse
{
    private Person _person;

    [XmlIgnore]
    public Person Person => _person;

    public static string NurseNickname { get; } = "Nurse Joy";
    
    private Pokecenter _pokecenter;

    [XmlIgnore]
    public Pokecenter Pokecenter
    {
        get => _pokecenter; 
        set
        {
            if (value == null) throw new ArgumentNullException("Pokecenter cannot be null.");
            _pokecenter = value;
        }
    }

    public Nurse()
    {
    }

    public Nurse(Person person, Pokecenter pokecenter)
    {
        _person = person ?? throw new ArgumentNullException(nameof(person));
        Pokecenter = pokecenter;
        pokecenter.Nurse = this;
    }
}