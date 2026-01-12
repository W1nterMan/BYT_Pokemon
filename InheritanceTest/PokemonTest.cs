using Models;

namespace InheritanceTest;

public class PokemonInheritanceTests
{
    [SetUp]
    public void Setup()
    {
    }
    
    private Nature _calm = new Nature("Calm", 1, 2);

    [Test]
    public void Pokemon_Type_And_EggType_MultiAspectTest()
    {
        Pokemon pokemonA = new PokemonBuilder(
                1, "pokemonA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _calm)
            .FireType(30)
            .LandEggType(10)
            .Build();
        
        Fire fire=pokemonA.Fire;
        Land land=pokemonA.Land;
        
        Assert.That(fire.Pokemon, Is.EqualTo(pokemonA));
        Assert.That(fire.BodyTemperature, Is.EqualTo(30));
        
        Assert.That(land.Pokemon, Is.EqualTo(pokemonA));
        Assert.That(land.AutoHealPoint, Is.EqualTo(10));
        
        Assert.That(pokemonA.Water, Is.Null);
        Assert.That(pokemonA.Flying, Is.Null);
        Assert.That(pokemonA.Underwater, Is.Null);
    }

    [Test]
    public void Pokemon_Multi_Type_Test()
    {
        Pokemon pokemonA = new PokemonBuilder(
                1, "pokemonA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _calm)
            .FireType(30)
            .WaterType(false)
            .LandEggType(10)
            .Build();
        
        Assert.That(pokemonA.Water, Is.Not.Null);
        Assert.That(pokemonA.Fire, Is.Not.Null);
        Assert.That(pokemonA.Flying, Is.Null);
    }

    [Test]
    public void Pokemon_Inheritance_ThrowException_Test()
    {
        //without type and egg type
        Assert.Throws<ArgumentException>(() =>
            new PokemonBuilder(1, "pokemonA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _calm)
                .Build());
        
        //without type
        Assert.Throws<ArgumentException>(() =>
            new PokemonBuilder(1, "pokemonA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _calm)
                .UnderwaterEggType()
                .Build());
        
        //without egg type
        Assert.Throws<ArgumentException>(() =>
            new PokemonBuilder(1, "pokemonA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _calm)
                .FireType(100)
                .Build());
        
        //Multi egg type
        Assert.Throws<ArgumentException>(() =>
            new PokemonBuilder(1, "pokemonA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _calm)
                .FireType(100)
                .UnderwaterEggType()
                .LandEggType(10)
                .Build());
    }
}