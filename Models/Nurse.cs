using Models;
using TestProject3.Models;

namespace TestProject6.BYT_Pokemon.Models;

[Serializable]
public class Nurse : Person
{
    private static List<Nurse> _extent = new List<Nurse>();
    public static string NurseNickname { get; } = "Nurse Joy";

    public Nurse()
    {
        AddNurse(this);
    }

    public Nurse(string name, int age)
    {
        Name = name;
        Age = age;
        AddNurse(this);
    }

    private static void AddNurse(Nurse nurse)
    {
        if (nurse == null)
            throw new ArgumentException("Nurse cannot be null.");
        _extent.Add(nurse);
    }

    public static List<Nurse> GetNurses()
    {
        return _extent;
    }

    public new static void Save(string path = "nurses.xml")
    {
        Serializer.Save(path, _extent);
    }

    public new static bool Load(string path = "nurses.xml")
    {
        return Serializer.Load(path, _extent);
    }
    
    public override void Contact()
    {
    }
}