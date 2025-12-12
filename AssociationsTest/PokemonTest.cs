using Models;

namespace AssociationsTest;

public class PokemonTest
{
    [Test]
    public void Pokemon_Nullable_EvolvesTo_Test()
    {
        Nature brave = new Nature("Brave", 1, 2);
        Pokemon pokemonA=new Pokemon(
            1,"pokemonA",100,100,100,[1,1,1,1,1,1],brave);
        Pokemon pokemonB=new Pokemon(
            2,"pokemonB",100,100,100,[1,1,1,1,1,1],brave);
        Assert.IsNull(pokemonA.EvolvesTo);
        pokemonA.EvolvesTo = pokemonB;
        Assert.IsNotNull(pokemonA.EvolvesTo);
    }

    [Test]
    public void Pokemon_EvolvesTo_Test_ThrowsException()
    {
        Nature lonely = new Nature("Lonely", 1, 2);
        Pokemon pokemonA=new Pokemon(
            1,"pokemonA",100,100,100,[1,1,1,1,1,1],lonely);
        Assert.IsNull(pokemonA.EvolvesTo);
        Assert.Throws<ArgumentException>(()=>pokemonA.EvolvesTo = pokemonA);
    }
}