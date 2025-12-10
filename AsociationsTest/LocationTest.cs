using Models;

namespace AsociationsTest;

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
        var road1 = new Road(1, TerrainType.Field);

        loc1.AddRoad(road1);
        
        Assert.That(loc1.GetLocationRoads().Count, Is.EqualTo(1));
    }
    
    [Test]
    public void RemoveRoadFromLocationTest()
    {
        var loc1 = new Location("Village 1", 50, 50, LocationType.Village);
        var loc2 = new Location("Village 2", 10, 10, LocationType.Village);
        var road1 = new Road(1, TerrainType.Field);

        loc1.AddRoad(road1);
        loc2.AddRoad(road1);
        loc1.RemoveRoad(road1);
        
        Assert.That(loc1.GetLocationRoads().Count, Is.EqualTo(0));
        Assert.That(loc2.GetLocationRoads().Count, Is.EqualTo(1));
        Assert.That(road1.GetRoadLocations().Count, Is.EqualTo(1));
    }
    
    
}