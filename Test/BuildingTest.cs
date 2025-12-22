using Models;

namespace Test;

public class BuildingTest
{
    private Location _testLocation;
    [SetUp]
    public void Setup()
    {
        _testLocation = new Location("Town", 10, 10, LocationType.Town);
    }
    //Basic attr
    [Test]
    public void Building_Name_Validation_ThrowsException()
    {
        var ex = Assert.Throws<ArgumentException>(() => new Shop("", true, 1.0, _testLocation));
        Assert.AreEqual("Building name cannot be empty or null.", ex.Message);
    }

    // Multi val attr
    [Test]
    public void Shop_MultiValue_ItemsSold_WorksCorrectly()
    {
        var shop = new Shop("Shop1", true, 1.0, _testLocation);
        
        shop.AddItem("Pokeball");
        shop.AddItem("Ultra Pokeball");

        Assert.AreEqual(2, shop.ItemsSold.Count);
        Assert.Contains("Pokeball", shop.ItemsSold);
        
        Assert.Throws<ArgumentException>(() => shop.AddItem(""));
    }

    // Static attr
    [Test]
    public void Pokecenter_StaticAttribute_IsShared()
    {
        Pokecenter.BaseHealingCost = 50;

        var center1 = new Pokecenter("Center 1", true, _testLocation, 99,"Joy",100);
        var center2 = new Pokecenter("Center 2", true, _testLocation, 11, "Joy",100);
        
        Assert.AreEqual(50, Pokecenter.BaseHealingCost);
        
        Pokecenter.BaseHealingCost = 100;
        Assert.AreEqual(100, Pokecenter.BaseHealingCost);
    }

    // Derived attr
    [Test]
    public void Gym_DerivedAttribute_CalculatesOnFly()
    {
        var gym = new Gym("Gym 1", true, "Leader 1", _testLocation);
        
        Assert.AreEqual(0, gym.TrainersCount);

        gym.TrainersInGym.Add("Trainer 1");
        gym.TrainersInGym.Add("Trainer 2");

        Assert.AreEqual(2, gym.TrainersCount);
    }

    // Optional attr
    [Test]
    public void Gym_OptionalAttribute_CanBeNull()
    {
        var gym = new Gym("Gym 1", true, "Leader 1", _testLocation);
        
        Assert.IsNull(gym.BadgeName);

        gym.BadgeName = "Badge 1";
        Assert.IsNotNull(gym.BadgeName);
    }
    
    [Test]
    public void Building_Extent_Saves_And_Loads_Subclasses_Correctly()
    {
        string TestPath = "test_buildings.xml";
        
        if (File.Exists(TestPath)) File.Delete(TestPath);

        var shop = new Shop("Store 1", true, 1.5, _testLocation);
        shop.AddItem("Item 1"); 

        var gym = new Gym("Gym 1", false, "Leader 1", _testLocation);
        gym.MinRequiredBadges = 5; 
        
        var initialExtent = Building.GetExtent();
        Assert.IsTrue(initialExtent.Count >= 2);
        
        Building.Save(TestPath);
        
        bool loadSuccess = Building.Load(TestPath);

        Assert.IsTrue(loadSuccess, "Load should return true");
        
        var loadedExtent = Building.GetExtent();
        
        var loadedShop = loadedExtent.OfType<Shop>().FirstOrDefault(s => s.Name == "Store 1");
        
        Assert.IsNotNull(loadedShop, "Shop should be retrieved");
        Assert.AreEqual(1.5, loadedShop.PriceMultiplier);
        Assert.Contains("Item 1", loadedShop.ItemsSold, "Shop inventory should persist");
        
        var loadedGym = loadedExtent.OfType<Gym>().FirstOrDefault(g => g.Name == "Gym 1");
        Assert.IsNotNull(loadedGym, "Gym should be retrieved");
        Assert.AreEqual("Leader 1", loadedGym.Leader);
        Assert.IsFalse(loadedGym.IsAccessible, "Gym accessibility should be false");
    }
}