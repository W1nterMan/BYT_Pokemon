using Models;

namespace AssociationsTest;


public class PokecenterTest
{
    
    [Test]
    public void Pokecenter_PC_Composition_Test()
    {
        var city = new Location("City", 1, 1, LocationType.City);
        var center = new Pokecenter("Pokecenter", true, city, 99);
        
        Assert.IsNotNull(center.Pc);
        Assert.AreEqual(99, center.Pc.ComputerNumber);
        Assert.AreEqual(center, center.Pc.Pokecenter);
        
        center.DeletePokecenter();
        
        Assert.IsNull(center.Pc); 
    }
}