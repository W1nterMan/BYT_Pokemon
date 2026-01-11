using Models;

namespace InheritanceTest;

public class BuildingTest
{
    private Location _testLocation;

    [SetUp]
    public void Setup()
    {
        _testLocation = new Location("Town", 0, 0, LocationType.Town);
    }

    [Test]
    public void Building_Shop_Test()
    {
        Building building = new BuildingBuilder("Shop", true, _testLocation)
            .AsShop(1.5)
            .Build();
        
        Shop shop = building.Shop;
        
        Assert.That(shop.Building, Is.EqualTo(building));
        Assert.That(shop.PriceMultiplier, Is.EqualTo(1.5));
        
        Assert.That(building.Gym, Is.Null);
        Assert.That(building.Pokecenter, Is.Null);
    }

    [Test]
    public void Building_Gym_Test()
    {
        Building building = new BuildingBuilder("Gym", true, _testLocation)
            .AsGym("Leader", "Badge")
            .Build();
        
        Gym gym = building.Gym;
        
        Assert.That(gym.Building, Is.EqualTo(building));
        Assert.That(gym.Leader, Is.EqualTo("Leader"));
        Assert.That(gym.BadgeName, Is.EqualTo("Badge"));
        
        Assert.That(building.Shop, Is.Null);
        Assert.That(building.Pokecenter, Is.Null);
    }

    [Test]
    public void Building_Pokecenter_Test()
    {
        Building building = new BuildingBuilder("Center", true, _testLocation)
            .AsPokecenter(1, "Joy", 25)
            .Build();
        
        Pokecenter pc = building.Pokecenter;
        
        Assert.That(pc.Building, Is.EqualTo(building));
        Assert.That(pc.Pc.ComputerNumber, Is.EqualTo(1));
        
        Assert.That(building.Shop, Is.Null);
        Assert.That(building.Gym, Is.Null);
    }

    [Test]
    public void Building_Inheritance_ThrowException_Test()
    {
        Assert.Throws<ArgumentException>(() =>
            new BuildingBuilder("Name", true, _testLocation)
                .Build());
        
        Assert.Throws<ArgumentException>(() =>
            new BuildingBuilder("Name", true, _testLocation)
                .AsShop(1.0)
                .AsGym("Leader", "Badge")
                .Build());
    }
}