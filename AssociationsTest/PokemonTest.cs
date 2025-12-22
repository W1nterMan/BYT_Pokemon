using Models;

namespace AssociationsTest;

public class PokemonTest
{
    [Test]
    public void Pokemon_Nullable_EvolvesTo_Test()
    {
        Nature lax = new Nature("Lax", 1, 2);
        Pokemon pokemonA = new Pokemon(
            1, "pokemonA", 100, 100, 100, [1, 1, 1, 1, 1, 1], lax);
        Pokemon pokemonB = new Pokemon(
            2, "pokemonB", 100, 100, 100, [1, 1, 1, 1, 1, 1], lax);
        Assert.IsNull(pokemonA.EvolvesTo);
        pokemonA.EvolvesTo = pokemonB;
        Assert.IsNotNull(pokemonA.EvolvesTo);
    }

    [Test]
    public void Pokemon_EvolvesTo_Test_ThrowsException()
    {
        Nature relaxed = new Nature("Relaxed", 1, 2);
        Pokemon pokemonA = new Pokemon(
            1, "pokemonA", 100, 100, 100, [1, 1, 1, 1, 1, 1], relaxed);
        Assert.IsNull(pokemonA.EvolvesTo);
        Assert.Throws<ArgumentException>(() => pokemonA.EvolvesTo = pokemonA);
    }

    [Test]
    public void Add_Bush_To_Pokemon_Test()
    {
        var loc1 = new Location("Village 1", 50, 50, LocationType.Village);

        var road = new Road(101, TerrainType.Field, loc1);

        var bush1 = new Bush(true, road);

        Nature rash = new Nature("Rash", 1, 2);
        Pokemon pokemonA = new Pokemon(
            1, "pokemonA", 100, 100, 100, [1, 1, 1, 1, 1, 1], rash);

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

        Nature quiet = new Nature("Quiet", 1, 2);
        Pokemon pokemonA = new Pokemon(
            1, "pokemonA", 100, 100, 100, [1, 1, 1, 1, 1, 1], quiet);

        pokemonA.AddBush(bush1);
        pokemonA.RemoveBush(bush1);
        Assert.That(pokemonA.GetBushes().Count, Is.EqualTo(0));
        Assert.That(bush1.GetPokemons().Count, Is.EqualTo(0));
    }
}