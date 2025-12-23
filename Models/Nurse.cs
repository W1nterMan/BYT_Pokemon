using System.Xml.Serialization;
using Models;

namespace Models;

[Serializable]
public class Nurse : Person
{
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

    public Nurse(string name, int age, Pokecenter pokecenter) : base(name, age)
    {
        Pokecenter = pokecenter;
        pokecenter.Nurse = this;
    }
}