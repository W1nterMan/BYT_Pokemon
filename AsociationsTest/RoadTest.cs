using Models;

namespace AsociationsTest;

public class RoadTest
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void AddLocationToRoadTest()
    {
        var loc1 = new Location("Village 1", 50, 50, LocationType.Village);
        var road1 = new Road(1, TerrainType.Field);

        road1.AddLocation(loc1);
        
        Assert.That(road1.GetRoadLocations().Count, Is.EqualTo(1));
    }
    
    [Test]
    public void RemoveLocationFromRoadTest()
    {
        var loc1 = new Location("Village 1", 50, 50, LocationType.Village);
        var loc2 = new Location("Village 2", 10, 10, LocationType.Village);
        var road1 = new Road(1, TerrainType.Field);

        road1.AddLocation(loc1);
        road1.AddLocation(loc2);
        road1.RemoveLocation(loc1);
        
        Assert.That(road1.GetRoadLocations().Count, Is.EqualTo(1));
        Assert.That(loc1.GetLocationRoads().Count, Is.EqualTo(0));
        Assert.That(loc2.GetLocationRoads().Count, Is.EqualTo(1));
    }
    
    [Test]
    public void RemoveLocationFromRoadExceptionTest()
    {
        var loc1 = new Location("Village 1", 50, 50, LocationType.Village);
        var road1 = new Road(1, TerrainType.Field);

        road1.AddLocation(loc1);
        
        Assert.Throws<Exception>(() => road1.RemoveLocation(loc1));
    }
}