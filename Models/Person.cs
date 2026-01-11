using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Models;

[Serializable]
public class Person
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
                throw new ArgumentException("Name can not be empty or whitespaces.");
            _name = value;
        }
    }

    public int Age
    {
        get => _age;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(Age), "Age can not be negative or zero.");
            _age = value;
        }
    }

    // Trainer and Leader - overlapping dynamic
    // Nurse - disjoint

    private Trainer? _trainer;
    private Leader? _leader;
    private Nurse? _nurse;

    public Trainer? Trainer => _trainer;
    public Leader? Leader => _leader;
    public Nurse? Nurse => _nurse;
    
    public Person() { }
    
    public Person Initialize(PersonBuilder builder)
    {
        Name = builder.Name;
        Age = builder.Age;
        
        if (builder.Nurse != null &&
            (builder.Trainer != null || builder.Leader != null))
        {
            throw new ArgumentException(
                "Nurse cannot overlap with Trainer or Leader.");
        }
        
        // overlapping dynamic
        _trainer = builder.Trainer;
        _leader = builder.Leader;
        _nurse = builder.Nurse;

        AddPerson(this);
        return this;
    }
    
    private static void AddPerson(Person person)
    {
        if (person == null)
            throw new ArgumentException("Person cannot be null.");
        _extent.Add(person);
    }

    public static List<Person> GetPersons()
    {
        return new List<Person>(_extent);
    }
    
    public static void Save(string path = "persons.xml")
    {
        Serializer.Save(path, _extent);
    }

    public static bool Load(string path = "persons.xml")
    {
        var loadedList = Serializer.Load(path, _extent);
        
        if (loadedList != null)
        {
            _extent = loadedList;
            return true;
        }
        return false;
    }
    
    public static void RemoveFromExtent(Person person) => _extent.Remove(person);
    
    public virtual void Contact() { }
}

public class PersonBuilder
{
    private readonly Person _person = new Person();

    public string Name { get; }
    public int Age { get; }

    public Trainer? Trainer { get; private set; }
    public Leader? Leader { get; private set; }
    public Nurse? Nurse { get; private set; }

    public PersonBuilder(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public PersonBuilder AsTrainer(
        int trainerId,
        int money,
        string[] badges,
        string? status)
    {
        Trainer = new Trainer(_person, trainerId, money, badges, status);
        return this;
    }
    
    
    public PersonBuilder AsLeader(string prefix, Team team)
    {
        Leader = new Leader(_person, prefix, team);
        return this;
    }


    public PersonBuilder AsNurse(Pokecenter pokecenter)
    {
        Nurse = new Nurse(_person, pokecenter);
        return this;
    }

    public Person Build()
    {
        return _person.Initialize(this);
    }
}
