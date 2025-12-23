using Models;

namespace AssociationsTest;

public class PokemonInBagTest
{
    [Test]
    public void PokemonInBag_Add_Test()
    {
        Nature adamant = new Nature("Adamant",1,2);
        Fire firePokemonA = new Fire(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], adamant, 100);
        Trainer trainer = new Trainer(123, 1234, new string[0], "Active", "Hanna", 32);
        Bag bagA = trainer.Bag;

        
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
        Nature naughty = new Nature("Naughty",0,3);
        Fire firePokemonA = new Fire(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], naughty, 100);
        Trainer trainer = new Trainer(1, 1000, new string[0], "Active", "Ash", 16);
        Bag bagA = trainer.Bag;

        
        PokemonInBag fire_bagA=new PokemonInBag(firePokemonA, bagA,"Ultra ball");
        PokemonInBag fire2_bagA=new PokemonInBag(firePokemonA, bagA,"Super ball");
        
        fire_bagA.RemovePokemonFromBag();
        
        Assert.That(firePokemonA.GetPokemonsInBag().Count,Is.EqualTo(1));
        Assert.That(bagA.OpenBag().Count,Is.EqualTo(1));
    }

    [Test]
    public void Bag_Exceed_Pokemon_Limit_ThrowException_Test()
    {
        Nature calm = new Nature("Calm",0,3);
        Fire firePokemon1 = new Fire(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], calm, 100);
        Trainer trainer = new Trainer(123, 1234, new string[0], "Active", "Hanna", 32);
        Bag bagA = trainer.Bag;

        
        PokemonInBag p1_bagA=new PokemonInBag(firePokemon1, bagA,"Ultra ball");
        PokemonInBag p2_bagA=new PokemonInBag(firePokemon1, bagA,"Ultra ball");
        PokemonInBag p3_bagA=new PokemonInBag(firePokemon1, bagA,"Ultra ball");
        PokemonInBag p4_bagA=new PokemonInBag(firePokemon1, bagA,"Ultra ball");
        PokemonInBag p5_bagA=new PokemonInBag(firePokemon1, bagA,"Ultra ball");
        PokemonInBag p6_bagA=new PokemonInBag(firePokemon1, bagA,"Ultra ball");
        Assert.Throws<InvalidOperationException>(()=>
            {
                PokemonInBag p7_bagA = new PokemonInBag(firePokemon1, bagA, "Ultra ball");
            }
            );
    }
}