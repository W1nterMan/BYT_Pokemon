using Models;

namespace AssociationsTest;

public class RoadTest
{
    [Test]
    public void Road_Reflexive_Connection_Test()
    {
        Road r1 = new Road(1, TerrainType.Field);
        Road r2 = new Road(2, TerrainType.Mountains);
        Road r3 = new Road(3, TerrainType.Cave);
        
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
}