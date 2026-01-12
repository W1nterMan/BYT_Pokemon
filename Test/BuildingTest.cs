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
        var ex = Assert.Throws<ArgumentException>(() => 
            new BuildingBuilder("", true, _testLocation).AsShop(1.0).Build());
        Assert.AreEqual("Building name cannot be empty or null.", ex.Message);
    }

    // Multi val attr
    [Test]
    public void Shop_MultiValue_ItemsSold_WorksCorrectly()
    {
        var building = new BuildingBuilder("Shop1", true, _testLocation).AsShop(1.0).Build();
        
        building.Shop.AddItem("Pokeball");
        building.Shop.AddItem("Ultra Pokeball");

        Assert.AreEqual(2, building.Shop.ItemsSold.Count);
        Assert.Contains("Pokeball", building.Shop.ItemsSold);
        
        Assert.Throws<ArgumentException>(() => building.Shop.AddItem(""));
    }

    // Static attr
    [Test]
    public void Pokecenter_StaticAttribute_IsShared()
    {
        Pokecenter.BaseHealingCost = 50;

        var center1 = new BuildingBuilder("Center 1", true, _testLocation).AsPokecenter(99, "Joy", 100).Build();
        var center2 = new BuildingBuilder("Center 2", true, _testLocation).AsPokecenter(11, "Joy", 100).Build();
        
        Assert.AreEqual(50, Pokecenter.BaseHealingCost);
        
        Pokecenter.BaseHealingCost = 100;
        Assert.AreEqual(100, Pokecenter.BaseHealingCost);
    }

    // Derived attr
    [Test]
    public void Gym_DerivedAttribute_CalculatesOnFly()
    {
        var building = new BuildingBuilder("Gym 1", true, _testLocation).AsGym("Leader 1", "Badge").Build();
        
        Assert.AreEqual(0, building.Gym.TrainersCount);

        building.Gym.TrainersInGym.Add("Trainer 1");
        building.Gym.TrainersInGym.Add("Trainer 2");

        Assert.AreEqual(2, building.Gym.TrainersCount);
    }

    // Optional attr
    [Test]
    public void Gym_OptionalAttribute_CanBeNull()
    {
        var building = new BuildingBuilder("Gym 1", true, _testLocation).AsGym("Leader 1", null).Build();
        
        Assert.IsNull(building.Gym.BadgeName);

        building.Gym.BadgeName = "Badge 1";
        Assert.IsNotNull(building.Gym.BadgeName);
    }
    
    [Test]
    public void Building_Extent_Saves_And_Loads_Subclasses_Correctly()
    {
        string TestPath = "test_buildings.xml";
        
        if (File.Exists(TestPath)) File.Delete(TestPath);

        new BuildingBuilder("Store 1", true, _testLocation).AsShop(1.5).Build();
        new BuildingBuilder("Gym 1", false, _testLocation).AsGym("Leader 1", "Boulder").Build();
        
        var initialExtent = Building.GetExtent();
        Assert.AreEqual(2, initialExtent.Count);
        
        Building.Save(TestPath);
        
        bool loadSuccess = Building.Load(TestPath);

        Assert.IsTrue(loadSuccess);
        
        var loadedExtent = Building.GetExtent();
        
        var shopBuilding = loadedExtent.FirstOrDefault(b => b.Name == "Store 1");
        Assert.IsNotNull(shopBuilding?.Shop);
        Assert.AreEqual(1.5, shopBuilding.Shop.PriceMultiplier);
        
        var gymBuilding = loadedExtent.FirstOrDefault(b => b.Name == "Gym 1");
        Assert.IsNotNull(gymBuilding?.Gym);
        Assert.AreEqual("Leader 1", gymBuilding.Gym.Leader);
    }
}