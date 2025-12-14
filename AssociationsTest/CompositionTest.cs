using Models;

namespace AssociationsTest;


public class CompositionTest
{

    [Test]
    public void Location_Building_Composition_Test()
    {
        var town = new Location("Town", 10, 10, LocationType.Town);
        
        var shop = new Shop("Shop", true, 1.0, town);
        
        Assert.IsTrue(town.GetBuildings().Contains(shop));
        Assert.AreEqual(town, shop.Location);
        
        town.DeleteLocation();
        
        var buildingExtent = Building.GetExtent();
        Assert.IsFalse(buildingExtent.Contains(shop), "Shop should be deleted when Location is deleted");
    }

    [Test]
    public void Road_Bush_MinMultiplicity_Test()
    {
        var road = new Road(101, TerrainType.Field);
        
        var bush1 = new Bush(true, 0.5, road);
        var bush2 = new Bush(true, 0.5, road);
        
        Assert.AreEqual(2, road.GetBushes().Count);
        
        road.RemoveBush(bush1);
        Assert.AreEqual(1, road.GetBushes().Count);
        
        var exception = Assert.Throws<InvalidOperationException>(() => road.RemoveBush(bush2));
        Assert.That(exception.Message, Does.Contain("must have at least one Bush"));
    }

    [Test]
    public void Building_Cannot_Exist_Without_Location()
    {
        Assert.Throws<ArgumentNullException>(() => new Shop("Shop", true, 1.0, null));
    }
    
    [Test]
    public void Pokecenter_PC_Composition_Test()
    {
        var city = new Location("City", 1, 1, LocationType.City);
        var center = new Pokecenter("Pokecenter", true, city);
        
        center.addPC(99);
        
        Assert.IsNotNull(center.Pc);
        Assert.AreEqual(99, center.Pc.ComputerNumber);
        Assert.AreEqual(center, center.Pc.Pokecenter);
        
        center.DeletePokecenter();
        
        Assert.IsNull(center.Pc); 
    }
}