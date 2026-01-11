using Models;

namespace AssociationsTest;

public class PokecenterTest
{
    [Test]
    public void Pokecenter_PC_Composition_Test()
    {
        var city = new Location("City", 1, 1, LocationType.City);
        var building = new BuildingBuilder("Pokecenter", true, city).AsPokecenter(99, "Joy", 100).Build();

        Assert.IsNotNull(building.Pokecenter.Pc);
        Assert.AreEqual(99, building.Pokecenter.Pc.ComputerNumber);
        Assert.AreEqual(building.Pokecenter, building.Pokecenter.Pc.Pokecenter);

        building.DeleteBuilding();

        var extent = Building.GetExtent();
        Assert.IsFalse(extent.Contains(building));
    }

    [Test]
    public void Pokecenter_Nurse_Association_Test()
    {
        var city = new Location("City", 1, 1, LocationType.City);
        var building = new BuildingBuilder("Pokecenter", true, city).AsPokecenter(99, "Joy", 100).Build();
        
        var joy = new Nurse("Joy", 25, building.Pokecenter);

        Assert.That(building.Pokecenter.Nurse, Is.EqualTo(joy));
        Assert.That(joy.Pokecenter, Is.EqualTo(building.Pokecenter));
    }

    [Test]
    public void Pokecenter_Nurse_Association_ThrowException_Test()
    {
        Assert.Throws<ArgumentNullException>(() => new Nurse("joy", 26, null));
    }
}