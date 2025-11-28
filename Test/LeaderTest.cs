using System.Reflection;
using TestProject6.BYT_Pokemon.Models;

namespace TestProject6.BYT_Pokemon.Test;

public class LeaderTest
{
    [SetUp]
    public void Setup()
    {
        var field = typeof(Leader).GetField("_extent", BindingFlags.Static | BindingFlags.NonPublic);
        field.SetValue(null, new List<Leader>());
    }

    [Test]
    public void Leader_Attributes()
    {
        Leader leader = new Leader
        {
            Name = "Leader",
            Age = 5,
            TotalMoney = 50,
            SpecialPrefix = "Super"
        };

        Assert.That(leader.Name, Is.EqualTo("Leader"));
        Assert.That(leader.Age, Is.EqualTo(5));
        Assert.That(leader.TotalMoney, Is.EqualTo(50));
        Assert.That(leader.SpecialPrefix, Is.EqualTo("Super"));
    }

    [Test]
    public void Leader_InvalidAttributes()
    {
        Leader leader = new Leader();

        Assert.Throws<ArgumentException>(() => leader.Name = "");
        Assert.Throws<ArgumentOutOfRangeException>(() => leader.Age = -1);
        Assert.Throws<ArgumentOutOfRangeException>(() => leader.TotalMoney = -1);
        Assert.Throws<ArgumentException>(() => leader.SpecialPrefix = "");
    }

    [Test]
    public void Leader_Extent()
    {
        Leader leader = new Leader { Name = "Leader1" };
        var extent = Leader.GetLeaders();
        Assert.That(extent.Count, Is.EqualTo(1));
        Assert.That(extent.First().Name, Is.EqualTo("Leader1"));
    }

    [Test]
    public void Leader_Encapsulation()
    {
        Leader leader = new Leader { Name = "Leader2" };
        var extent = Leader.GetLeaders();
        leader.Name = "Ivan";
        Assert.That(extent.First().Name, Is.EqualTo("Ivan"));
    }

    [Test]
    public void Leader_Persistence()
    {
        string path = "leaders_test.xml";
        if (File.Exists(path)) File.Delete(path);

        Leader leader1 = new Leader { Name = "Ivan1" };
        Leader leader2 = new Leader { Name = "Ivan2" };

        Leader.Save(path);

        var field = typeof(Leader).GetField("_extent", BindingFlags.Static | BindingFlags.NonPublic);
        field.SetValue(null, new List<Leader>());

        bool loadSuccess = Leader.Load(path);
        Assert.IsTrue(loadSuccess);

        var loadedExtent = Leader.GetLeaders();
        Assert.That(loadedExtent.Count, Is.EqualTo(2));
        Assert.That(loadedExtent.Any(l => l.Name == "Ivan1"), Is.True);
        Assert.That(loadedExtent.Any(l => l.Name == "Ivan1"), Is.True);
    }
}
