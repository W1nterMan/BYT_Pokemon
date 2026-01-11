using Models;

namespace InheritanceTest;

public class PersonInheritanceTest
{
    
    [Test]
    public void AsTrainer_Test()
    {
        var person = new PersonBuilder("Hanna", 32).AsTrainer(123, 1234, [], "Active").Build();

        Assert.That(person.Trainer, Is.Not.Null);
        Assert.That(person.Nurse, Is.Null);
        Assert.That(person.Leader, Is.Null);
    }

    [Test]
    public void AsNurse_Test()
    {
        var location = new Location("Town", 0, 0, LocationType.Town);
        var building = new BuildingBuilder("PC", true, location).AsPokecenter(1, "Joy", 25).Build();

        var person = new PersonBuilder("Joy", 25).AsNurse(building.Pokecenter).Build();

        Assert.That(person.Nurse, Is.Not.Null);
        Assert.That(person.Trainer, Is.Null);
        Assert.That(person.Leader, Is.Null);
    }

    [Test]
    public void AsLeader_Test()
    {
        var team = new Team { Name = "Winners" };

        var person = new PersonBuilder("Hubba", 65).AsLeader("Win", team).Build();

        Assert.That(person.Leader, Is.Not.Null);
        Assert.That(person.Trainer, Is.Null);
        Assert.That(person.Nurse, Is.Null);
    }
    
    [Test]
    public void Overlapping_Test()
    {
        var team = new Team { Name = "Winners" };

        var person = new PersonBuilder("Hubba", 65).AsTrainer(123, 1234, [], "Active").AsLeader("Win", team).Build();

        Assert.That(person.Trainer, Is.Not.Null);
        Assert.That(person.Leader, Is.Not.Null);
        Assert.That(person.Nurse, Is.Null);
    }
    
    [Test]
    public void Nurse_Trainer_Disjoint_Test()
    {
        var location = new Location("City", 0, 0, LocationType.City);
        var building = new BuildingBuilder("PC", true, location).AsPokecenter(1, "Joy", 25).Build();

        Assert.Throws<ArgumentException>(() =>
            new PersonBuilder("Joy", 25).AsTrainer(1, 1000, [], "Active").AsNurse(building.Pokecenter).Build());
    }
    
    [Test]
    public void NoRole_Test()
    {
        var person = new PersonBuilder("Hubba", 65).Build();

        Assert.That(person.Trainer, Is.Null);
        Assert.That(person.Nurse, Is.Null);
        Assert.That(person.Leader, Is.Null);
    }
    
    [Test]
    public void ThrowException_Test()
    {
        Assert.Throws<ArgumentException>(() =>
            new PersonBuilder("", 65).Build());

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new PersonBuilder("Hubba", -65).Build());
    }
}
