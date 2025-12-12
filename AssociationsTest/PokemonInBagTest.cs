using Models;

namespace AssociationsTest;

public class PokemonInBagTest
{
    [Test]
    public void PokemonInBag_Add_Test()
    {
        Nature brave = new Nature("Brave",1,2);
        Fire firePokemonA = new Fire(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], brave, 100);
        Bag bagA = new Bag();
        
        PokemonInBag fire_bagA=new PokemonInBag(firePokemonA, bagA,"Ultra ball");
        PokemonInBag fire2_bagA=new PokemonInBag(firePokemonA, bagA,"Super ball");
        
        
        Assert.That(firePokemonA.GetPokemonsInBag().Contains(fire_bagA));
        Assert.That(firePokemonA.GetPokemonsInBag().Contains(fire2_bagA));
        Assert.That(bagA.OpenBag().Contains(fire_bagA));
        Assert.That(bagA.OpenBag().Contains(fire2_bagA));
    }

    [Test]
    public void PokemonInBag_Remove_Test()
    {
        Nature lonely = new Nature("Lonely",0,3);
        Fire firePokemonA = new Fire(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], lonely, 100);
        Bag bagA = new Bag();
        
        PokemonInBag fire_bagA=new PokemonInBag(firePokemonA, bagA,"Ultra ball");
        PokemonInBag fire2_bagA=new PokemonInBag(firePokemonA, bagA,"Super ball");
        
        fire_bagA.RemovePokemonFromBag();
        
        Assert.That(firePokemonA.GetPokemonsInBag().Count,Is.EqualTo(1));
        Assert.That(bagA.OpenBag().Count,Is.EqualTo(1));
    }
}