using System.Reflection;
using Models;

namespace Test;

public class NatureTest
{
    [SetUp]
    public void Setup()
    {
        var field =typeof(Nature).GetField("_extent", BindingFlags.Static|BindingFlags.NonPublic);
        field.SetValue(null, new List<Nature>());
    }
    
    [TestCase("",1,2)]
    [TestCase("Brave",6,2)]
    [TestCase("Brave",1,6)]
    [TestCase("Brave",1,1)]
    public void Nature_Invalid_Argument_ThrowException(string name,int raisedStat,int loweredStat)
    {
        Assert.Throws<ArgumentException>(()=>new Nature(name,raisedStat,loweredStat));
    }
    
    [Test]
    public void Nature_Duplicate_Name_ThrowException()
    {
        Nature brave= new Nature("Brave",1,2);
        Assert.Throws<ArgumentException>(()=>new Nature("Brave",1,3));
        Assert.That(Nature.GetNatures().Count, Is.EqualTo(1));
    }
    
    [Test]
    public void Nature_Extent_Test()
    {
        Nature brave = new Nature("Brave",1,2);
        Nature lonely = new Nature("Lonely",0,3);

        var extent = Nature.GetNatures();
        Assert.That(extent.Count, Is.EqualTo(2));
        Assert.That(extent.First(n=>n.Name=="Brave").Name, Is.EqualTo("Brave"));
        Assert.That(extent.First(n=>n.Name=="Lonely").Name, Is.EqualTo("Lonely"));
    }

    [Test]
    public void Nature_Encapsulation_Test()
    {
        Nature brave = new Nature("Brave",1,2);
        Nature lonely = new Nature("Lonely",0,3);

        var extent = Nature.GetNatures();
        Assert.That(extent.First(n=>n.Name=="Brave").Name, Is.EqualTo("Brave"));
        Assert.That(extent.First(n=>n.Name=="Lonely").Name, Is.EqualTo("Lonely"));
        
        brave.Name = "Naughty";
        lonely.Name = "Mild";
        
        Assert.That(extent.First(n=>n.Name=="Naughty").Name, Is.EqualTo("Naughty"));
        Assert.That(extent.First(n=>n.Name=="Mild").Name, Is.EqualTo("Mild"));
    }

    [Test]
    public void Nature_Persistence_Test()
    {
        string TestPath = "test_natures.xml";
        
        if (File.Exists(TestPath)) File.Delete(TestPath);

        Nature brave = new Nature("Brave",1,2);
        Nature lonely = new Nature("Lonely",0,3);
        
        var initialExtent = Nature.GetNatures();
        Assert.That(initialExtent.Count, Is.EqualTo(2));
        
        Nature.Save(TestPath);
        
        bool loadSuccess = Nature.Load(TestPath);
        
        Assert.IsTrue(loadSuccess, "Load should return true");
        
        var loadedExtent = Nature.GetNatures();
        
        var loadedBrave = loadedExtent.OfType<Nature>().FirstOrDefault(n => n.Name == "Brave");
        
        Assert.IsNotNull(loadedBrave, "Shop should be retrieved");
        Assert.That(loadedBrave.Name, Is.EqualTo("Brave"));
    }
}