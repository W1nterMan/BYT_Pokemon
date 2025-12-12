using Models;
namespace AssociationsTest;

public class NatureTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void AddPokemonToNatureTest()
    {
        Nature brave = new Nature("Brave",1,2);
        Fire firePokemonA = new Fire(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], brave, 100);
        Fire firePokemonB = new Fire(
            1, "fireB", 100, 100, 100, [1, 1, 1, 1, 1, 1], brave, 100);
        
        Assert.That(brave.GetNaturePokemons().Contains(firePokemonA));
        Assert.That(brave.GetNaturePokemons().Count, Is.EqualTo(2));
    }
    
    [Test]
    public void RemovePokemonFromNatureTest()
    {
        Nature lonely = new Nature("Lonely",0,3);
        Fire firePokemonA = new Fire(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], lonely, 100);
        Fire firePokemonB = new Fire(
            1, "fireB", 100, 100, 100, [1, 1, 1, 1, 1, 1], lonely, 100);
        
        lonely.RemovePokemon(firePokemonA);
        
        Assert.That(!lonely.GetNaturePokemons().Contains(firePokemonA));
        Assert.That(lonely.GetNaturePokemons().Count, Is.EqualTo(1));
    }
    
}