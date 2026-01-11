using Models;

namespace AssociationsTest;

public class LocationTest
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void AddRoadToLocationTest()
    {
        var loc1 = new Location("Village 1", 50, 50, LocationType.Village);
        var road1 = new Road(1, TerrainType.Field,loc1);
        
        loc1.AddRoad(road1);
        
        Assert.That(loc1.GetLocationRoads().Count, Is.EqualTo(1));
    }
    
    [Test]
    public void RemoveRoadFromLocationTest()
    {
        var loc1 = new Location("Village 1", 50, 50, LocationType.Village);
        var loc2 = new Location("Village 2", 50, 50, LocationType.Village);
        var road1 = new Road(1, TerrainType.Field,loc1);
        var road2 = new Road(2, TerrainType.Field,loc1);
        
        loc2.AddRoad(road2);
        loc1.RemoveRoad(road2);
        
        Assert.That(loc1.GetLocationRoads().Count, Is.EqualTo(1));
        Assert.That(road1.GetRoadLocations().Count, Is.EqualTo(1));
        Assert.That(road2.GetRoadLocations().Count, Is.EqualTo(1));
    }
    
    [Test]
    public void Location_Building_Composition_Test()
    {
        var town = new Location("Town", 10, 10, LocationType.Town);
        
        var building = new BuildingBuilder("Shop", true, town).AsShop(1.0).Build();
    
        Assert.IsTrue(town.GetBuildings().Contains(building));
        Assert.AreEqual(town, building.Location);
    
        town.DeleteLocation();
    
        var buildingExtent = Building.GetExtent();
        Assert.IsFalse(buildingExtent.Contains(building), "Building should be deleted when Location is deleted");
    }
    
    [Test]
    public void Building_Cannot_Exist_Without_Location()
    {
        Assert.Throws<ArgumentNullException>(() => new BuildingBuilder("Shop", true, null).AsShop(1.0).Build());
    }
    
    
}