using Models;

namespace AssociationsTest;

public class PokemonTest
{
    [Test]
    public void Pokemon_Nullable_EvolvesTo_Test()
    {
        Nature lax = new Nature("Lax", 1, 2);
        Pokemon pokemonA=new Pokemon(
            1,"pokemonA",100,100,100,[1,1,1,1,1,1],lax);
        Pokemon pokemonB=new Pokemon(
            2,"pokemonB",100,100,100,[1,1,1,1,1,1],lax);
        Assert.IsNull(pokemonA.EvolvesTo);
        pokemonA.EvolvesTo = pokemonB;
        Assert.IsNotNull(pokemonA.EvolvesTo);
    }

    [Test]
    public void Pokemon_EvolvesTo_Test_ThrowsException()
    {
        Nature relaxed = new Nature("Relaxed", 1, 2);
        Pokemon pokemonA=new Pokemon(
            1,"pokemonA",100,100,100,[1,1,1,1,1,1],relaxed);
        Assert.IsNull(pokemonA.EvolvesTo);
        Assert.Throws<ArgumentException>(()=>pokemonA.EvolvesTo = pokemonA);
    }
}