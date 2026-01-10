using Models;
namespace AssociationsTest;

public class NatureTest
{
    [SetUp]
    public void Setup()
    {
    }
    
    //TODO: remove is not working how it should.
    [Test]
    public void AddPokemonToNatureTest()
    {
        Nature brave = new Nature("Brave",1,2);
        Pokemon firePokemonA = new PokemonBuilder(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], brave)
            .FireType(100)
            .LandEggType(10)
            .Build();
        Pokemon firePokemonB = new PokemonBuilder(
            1, "fireB", 100, 100, 100, [1, 1, 1, 1, 1, 1], brave)
            .FireType(100)
            .LandEggType(10)
            .Build();
        Assert.That(brave.GetNaturePokemons().Contains(firePokemonA));
        Assert.That(brave.GetNaturePokemons().Count, Is.EqualTo(2));
    }
    
    [Test]
    public void RemovePokemonFromNatureTest()
    {
        Nature lonely = new Nature("Lonely",0,3);
        Pokemon firePokemonA = new PokemonBuilder(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], lonely)
            .FireType(100)
            .LandEggType(10)
            .Build();
        Pokemon firePokemonB = new PokemonBuilder(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], lonely)
            .FireType(100)
            .LandEggType(10)
            .Build();
        
        lonely.RemovePokemon(firePokemonA);
        
        Assert.That(!lonely.GetNaturePokemons().Contains(firePokemonA));
        Assert.That(lonely.GetNaturePokemons().Count, Is.EqualTo(1));
    }
    
}