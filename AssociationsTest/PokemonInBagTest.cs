using Models;

namespace AssociationsTest;

public class PokemonInBagTest
{
    private Nature _adamant = new Nature("Adamant", 1, 2);
    [Test]
    public void PokemonInBag_Add_Test()
    {
        Pokemon firePokemonA = new PokemonBuilder(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _adamant)
            .FireType(100)
            .LandEggType(10)
            .Build();
        
        Trainer trainer = new PersonBuilder("Hanna", 32).AsTrainer(123, 1234, new string[0], "Active").Build().Trainer!;
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
        Pokemon firePokemonA = new PokemonBuilder(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _adamant)
            .FireType(100)
            .LandEggType(10)
            .Build();
        Trainer trainer = new PersonBuilder("Ash", 16).AsTrainer(1, 1000, new string[0], "Active").Build().Trainer!;
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
        Pokemon firePokemon1 = new PokemonBuilder(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _adamant)
            .FireType(100)
            .LandEggType(10)
            .Build();
        Trainer trainer = new PersonBuilder("Hanna", 32).AsTrainer(123, 1234, new string[0], "Active").Build().Trainer!;
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