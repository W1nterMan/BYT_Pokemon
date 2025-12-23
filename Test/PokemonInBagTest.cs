using System.Reflection;
using Models;

namespace Test;

public class PokemonInBagTest
{
    private Nature brave;
    private Pokemon pikachu;
    
    [SetUp]
    public void Setup()
    {
        var pib =typeof(PokemonInBag).GetField("_extent", BindingFlags.Static|BindingFlags.NonPublic);
        pib.SetValue(null, new List<PokemonInBag>());
        
        var nature =typeof(Nature).GetField("_extent", BindingFlags.Static|BindingFlags.NonPublic);
        nature.SetValue(null, new List<Nature>());
        
        brave = new Nature("Brave", 1, 2);
        pikachu = new Pokemon(1, "Charmander", 20, 1, 40, new int[]{1,1,1,1,1,1}, brave);
    }
    
    [Test]
    public void PokemonInBag_Invalid_Argument_ThrowException()
    {
        Trainer trainer = new Trainer(123, 1234, new string[0], "Active", "Hanna", 32);
        Bag bag = trainer.Bag;

        Assert.Throws<ArgumentException>(()=>new PokemonInBag(pikachu,bag,""));
    }
    
    [Test]
    public void PokemonInBag_Extent_Test()
    {
        Trainer trainer = new Trainer(123, 1234, new string[0], "Active", "Hanna", 32);
        Bag bag = trainer.Bag;

        PokemonInBag pokemonA = new PokemonInBag(pikachu,bag,"Ultra Ball");
        PokemonInBag pokemonB = new PokemonInBag(pikachu,bag,"Special Ball");

        var extent = PokemonInBag.GetPokemonsInBag();
        Assert.That(extent.Count, Is.EqualTo(2));
        Assert.That(extent.First(p=>p.Pokeball=="Ultra Ball").Pokeball, Is.EqualTo("Ultra Ball"));
        Assert.That(extent.First(p=>p.Pokeball=="Special Ball").Pokeball, Is.EqualTo("Special Ball"));
    }

    [Test]
    public void PokemonInBag_Encapsulation_Test()
    {
        Trainer trainer = new Trainer(123, 1234, new string[0], "Active", "Hanna", 32);
        Bag bag = trainer.Bag;

        PokemonInBag pokemonA = new PokemonInBag(pikachu,bag,"Ultra Ball");
        PokemonInBag pokemonB = new PokemonInBag(pikachu,bag,"Special Ball");

        var extent = PokemonInBag.GetPokemonsInBag();
        Assert.That(extent.First(p=>p.Pokeball=="Ultra Ball").Pokeball, Is.EqualTo("Ultra Ball"));
        Assert.That(extent.First(p=>p.Pokeball=="Special Ball").Pokeball, Is.EqualTo("Special Ball"));
        
        pokemonA.Pokeball = "Normal Ball";
        pokemonB.Pokeball = "Super Ball";
        
        Assert.That(extent.First(p=>p.Pokeball=="Normal Ball").Pokeball, Is.EqualTo("Normal Ball"));
        Assert.That(extent.First(p=>p.Pokeball=="Super Ball").Pokeball, Is.EqualTo("Super Ball"));
    }

    [Test]
    public void PokemonInBag_Persistence_Test()
    {
        Trainer trainer = new Trainer(123, 1234, new string[0], "Active", "Hanna", 32);
        Bag bag = trainer.Bag;

        string TestPath = "test_pokemons_in_bag.xml";
        
        if (File.Exists(TestPath)) File.Delete(TestPath);

        PokemonInBag pokemonA = new PokemonInBag(pikachu,bag,"Ultra Ball");
        PokemonInBag pokemonB = new PokemonInBag(pikachu,bag,"Special Ball");
        
        var initialExtent = PokemonInBag.GetPokemonsInBag();
        Assert.That(initialExtent.Count, Is.EqualTo(2));
        
        PokemonInBag.Save(TestPath);

        bool loadSuccess = PokemonInBag.Load(TestPath);
        
        Assert.IsTrue(loadSuccess, "Load should return true");
        
        var loadedExtent = PokemonInBag.GetPokemonsInBag();
        
        var loadedPokemonA = loadedExtent.OfType<PokemonInBag>().FirstOrDefault(p => p.Pokeball == "Ultra Ball");
        
        Assert.IsNotNull(loadedPokemonA, "PokemonA should be retrieved");
        Assert.That(loadedPokemonA.Pokeball, Is.EqualTo("Ultra Ball"));
    }
}