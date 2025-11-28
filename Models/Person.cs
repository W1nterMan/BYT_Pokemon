using System.Xml;
using System.Xml.Serialization;
using Models;

namespace TestProject3.Models;

[Serializable]
public abstract class Person
{
    private static List<Person> _extent = new List<Person>();
    private string _name;
    private int _age;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name can not be empty.");
            _name = value;
        }
    }

    public int Age
    {
        get => _age;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(Age), "Age can not be empty.");
            _age = value;
        }
    }
    
    public Person() {}

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
        AddPerson(this);
    }

    private static void AddPerson(Person person)
    {
        if (person == null)
            throw new ArgumentException("Person cannot be null.");
        _extent.Add(person);
    }

    public static List<Person> GetPersons()
    {
        return _extent;
    }

    public static void Save(string path = "persons.xml")
    {
        Serializer.Save(path, _extent);
    }

    public static bool Load(string path = "persons.xml")
    {
        return Serializer.Load(path, _extent);
    }

    public virtual void Contact() { }
}