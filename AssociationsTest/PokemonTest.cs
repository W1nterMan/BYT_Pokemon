using Models;

namespace AssociationsTest;

public class PokemonTest
{
    private Nature _lax = new Nature("Lax", 1, 2);
    [Test]
    public void Pokemon_Nullable_EvolvesTo_Test()
    {
        Pokemon pokemonA = new PokemonBuilder(
            1, "pokemonA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _lax)
            .FireType(30)
            .LandEggType(10)
            .Build();
        Pokemon pokemonB = new PokemonBuilder(
            2, "pokemonB", 100, 100, 100, [1, 1, 1, 1, 1, 1], _lax)
            .FireType(30)
            .LandEggType(10)
            .Build();
        Assert.IsNull(pokemonA.EvolvesTo);
        pokemonA.EvolvesTo = pokemonB;
        Assert.IsNotNull(pokemonA.EvolvesTo);
    }

    [Test]
    public void Pokemon_EvolvesTo_Test_ThrowsException()
    {
        Pokemon pokemonA = new PokemonBuilder(
            1, "pokemonA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _lax)
            .FireType(30)
            .LandEggType(10)
            .Build();
        Assert.IsNull(pokemonA.EvolvesTo);
        Assert.Throws<ArgumentException>(() => pokemonA.EvolvesTo = pokemonA);
    }

    [Test]
    public void Add_Bush_To_Pokemon_Test()
    {
        var loc1 = new Location("Village 1", 50, 50, LocationType.Village);

        var road = new Road(101, TerrainType.Field, loc1);

        var bush1 = new Bush(true, road);

        Pokemon pokemonA = new PokemonBuilder(
            1, "pokemonA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _lax)
            .FireType(30)
            .LandEggType(10)
            .Build();

        pokemonA.AddBush(bush1);

        Assert.That(pokemonA.GetBushes().Count, Is.EqualTo(1));
        Assert.That(bush1.GetPokemons().Count, Is.EqualTo(1));
        Assert.IsTrue(pokemonA.GetBushes().Contains(bush1));
        Assert.IsTrue(bush1.GetPokemons().Contains(pokemonA));
    }

    [Test]
    public void Remove_Bush_From_Pokemon_Test()
    {
        var loc1 = new Location("Village 1", 50, 50, LocationType.Village);

        var road = new Road(101, TerrainType.Field, loc1);

        var bush1 = new Bush(true, road);

        Pokemon pokemonA = new PokemonBuilder(
            1, "pokemonA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _lax)
            .FireType(30).LandEggType(10).Build();

        pokemonA.AddBush(bush1);
        pokemonA.RemoveBush(bush1);
        Assert.That(pokemonA.GetBushes().Count, Is.EqualTo(0));
        Assert.That(bush1.GetPokemons().Count, Is.EqualTo(0));
    }
}