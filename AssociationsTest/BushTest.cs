using Models;

namespace AssociationsTest;

public class BushTest
{
    [Test]
    public void Add_Pokemon_To_Bush_Test()
    {
        var loc1 = new Location("Village 1", 50, 50, LocationType.Village);

        var road = new Road(101, TerrainType.Field, loc1);

        var bush1 = new Bush(true, road);

        Nature gentle = new Nature("Gentle", 1, 2);
        Pokemon pokemonA = new Pokemon(
            1, "pokemonA", 100, 100, 100, [1, 1, 1, 1, 1, 1], gentle);

        bush1.AddPokemon(pokemonA);

        Assert.That(bush1.GetPokemons().Count, Is.EqualTo(1));
        Assert.That(pokemonA.GetBushes().Count, Is.EqualTo(1));
        Assert.IsTrue(pokemonA.GetBushes().Contains(bush1));
        Assert.IsTrue(bush1.GetPokemons().Contains(pokemonA));
    }

    [Test]
    public void Remove_Pokemon_From_Bush_Test()
    {
        var loc1 = new Location("Village 1", 50, 50, LocationType.Village);

        var road = new Road(101, TerrainType.Field, loc1);

        var bush1 = new Bush(true, road);

        Nature careful = new Nature("Careful", 1, 2);
        Pokemon pokemonA = new Pokemon(
            1, "pokemonA", 100, 100, 100, [1, 1, 1, 1, 1, 1], careful);

        bush1.AddPokemon(pokemonA);
        bush1.RemovePokemon(pokemonA);
        Assert.That(pokemonA.GetBushes().Count, Is.EqualTo(0));
        Assert.That(bush1.GetPokemons().Count, Is.EqualTo(0));
    }
}