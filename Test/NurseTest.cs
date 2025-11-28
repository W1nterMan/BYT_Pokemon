using System.Reflection;
using TestProject6.BYT_Pokemon.Models;

namespace TestProject6.BYT_Pokemon.Test;

public class NurseTest
{
    [SetUp]
    public void Setup()
    {
        var field = typeof(Nurse).GetField("_extent", BindingFlags.Static | BindingFlags.NonPublic);
        field.SetValue(null, new List<Nurse>());
    }

    [Test]
    public void Nurse_Attributes()
    {
        Nurse nurse = new Nurse { Name = "Joy", Age = 132 };
        Assert.That(nurse.Name, Is.EqualTo("Joy"));
        Assert.That(nurse.Age, Is.EqualTo(132));
    }

    [Test]
    public void Nurse_InvalidAttributes()
    {
        Nurse nurse = new Nurse();
        Assert.Throws<ArgumentException>(() => nurse.Name = "");
        Assert.Throws<ArgumentOutOfRangeException>(() => nurse.Age = -1);
    }

    [Test]
    public void Nurse_Extent()
    {
        Nurse joy = new Nurse { Name = "Joy" };
        var extent = Nurse.GetNurses();
        Assert.That(extent.Count, Is.EqualTo(1));
        Assert.That(extent.First().Name, Is.EqualTo("Joy"));
    }

    [Test]
    public void Nurse_Encapsulation()
    {
        Nurse nurse = new Nurse { Name = "Joy" };
        var extent = Nurse.GetNurses();
        nurse.Name = "Lidia";
        Assert.That(extent.First().Name, Is.EqualTo("Lidia"));
    }

    [Test]
    public void Nurse_Persistence()
    {
        string path = "nurses_test.xml";
        if (File.Exists(path)) File.Delete(path);

        Nurse lily = new Nurse { Name = "Lily" };
        Nurse lilly = new Nurse { Name = "Lilly" };

        Nurse.Save(path);

        var field = typeof(Nurse).GetField("_extent", BindingFlags.Static | BindingFlags.NonPublic);
        field.SetValue(null, new List<Nurse>());

        bool loadSuccess = Nurse.Load(path);
        Assert.IsTrue(loadSuccess);

        var loadedExtent = Nurse.GetNurses();
        Assert.That(loadedExtent.Count, Is.EqualTo(2));
        Assert.That(loadedExtent.Any(n => n.Name == "Lily"), Is.True);
        Assert.That(loadedExtent.Any(n => n.Name == "Lilly"), Is.True);
    }
}
