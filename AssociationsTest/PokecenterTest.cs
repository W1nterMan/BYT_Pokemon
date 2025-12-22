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

    [Test]
    public void Pokecenter_Nurse_Association_Test()
    {
        var city = new Location("City", 1, 1, LocationType.City);
        var center = new Pokecenter("Pokecenter", true, city, 99);
        var joy = new Nurse("Joy", 25, center);

        Assert.That(center.Nurse, Is.EqualTo(joy));
        Assert.That(joy.Pokecenter, Is.EqualTo(center));
    }

    [Test]
    public void Pokecenter_Nurse_Association_ThrowException_Test()
    {
        Assert.Throws<ArgumentNullException>(() => new Nurse("joy", 26, null));
    }
}