using Models;

namespace AssociationsTest;

public class RoadTest
{
    [Test]
    public void AddLocationToRoadTest()
    {
        var loc1 = new Location("Village 1", 50, 50, LocationType.Village);
        var loc2 = new Location("Village 2", 50, 50, LocationType.Village);
        var road1 = new Road(1, TerrainType.Field,loc2);

        road1.AddLocation(loc1);
        
        Assert.That(road1.GetRoadLocations().Count, Is.EqualTo(2));
    }
    
    [Test]
    public void RemoveLocationFromRoadTest()
    {
        var loc1 = new Location("Village 1", 50, 50, LocationType.Village);
        var loc2 = new Location("Village 2", 10, 10, LocationType.Village);
        var road1 = new Road(1, TerrainType.Field,loc1);
        
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
        var road1 = new Road(1, TerrainType.Field,loc1);
        
        Assert.Throws<Exception>(() => road1.RemoveLocation(loc1));
    }
    
    [Test]
    public void Road_Reflexive_Connection_Test()
    {
        var loc1 = new Location("Village 1", 50, 50, LocationType.Village);
        
        Road r1 = new Road(1, TerrainType.Field, loc1);
        Road r2 = new Road(2, TerrainType.Mountains, loc1);
        Road r3 = new Road(3, TerrainType.Cave, loc1);
        
        r1.ConnectToRoad(r2);
        
        Assert.IsTrue(r1.GetConnectedRoads().Contains(r2));
        Assert.IsTrue(r2.GetConnectedRoads().Contains(r1));
        
        r2.ConnectToRoad(r3);
        Assert.IsTrue(r2.GetConnectedRoads().Contains(r3));
        
        r1.DisconnectFromRoad(r2);
        Assert.IsFalse(r1.GetConnectedRoads().Contains(r2));
        Assert.IsFalse(r2.GetConnectedRoads().Contains(r1));
        
        Assert.IsTrue(r2.GetConnectedRoads().Contains(r3));
    }
    
    [Test]
    public void Road_Bush_MinMultiplicity_Test()
    {
        var loc1 = new Location("Village 1", 50, 50, LocationType.Village);
        
        var road = new Road(101, TerrainType.Field, loc1);
        
        var bush1 = new Bush(true,road);
        var bush2 = new Bush(true,road);
        
        Assert.AreEqual(2, road.GetBushes().Count);
        
        road.RemoveBush(bush1);
        Assert.AreEqual(1, road.GetBushes().Count);
        
        var exception = Assert.Throws<InvalidOperationException>(() => road.RemoveBush(bush2));
        Assert.That(exception.Message, Does.Contain("must have at least one Bush"));
    }
}