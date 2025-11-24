using System.Reflection;
using Models;

namespace Test;

public class PokemonInBagTest
{
    [SetUp]
    public void Setup()
    {
        var field =typeof(PokemonInBag).GetField("_extent", BindingFlags.Static|BindingFlags.NonPublic);
        field.SetValue(null, new List<PokemonInBag>());
    }
    [Test]
    public void PokemonInBag_Invalid_Argument_ThrowException()
    {
        Assert.Throws<ArgumentException>(()=>new PokemonInBag(""));
    }
    
    [Test]
    public void PokemonInBag_Extent_Test()
    {
        PokemonInBag pokemonA = new PokemonInBag("Ultra Ball");
        PokemonInBag pokemonB = new PokemonInBag("Special Ball");

        var extent = PokemonInBag.GetPokemonsInBag();
        Assert.That(extent.Count, Is.EqualTo(2));
        Assert.That(extent.First(p=>p.Pokeball=="Ultra Ball").Pokeball, Is.EqualTo("Ultra Ball"));
        Assert.That(extent.First(p=>p.Pokeball=="Special Ball").Pokeball, Is.EqualTo("Special Ball"));
    }

    [Test]
    public void PokemonInBag_Encapsulation_Test()
    {
        PokemonInBag pokemonA = new PokemonInBag("Ultra Ball");
        PokemonInBag pokemonB = new PokemonInBag("Special Ball");

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
        string TestPath = "test_pokemons_in_bag.xml";
        
        if (File.Exists(TestPath)) File.Delete(TestPath);

        PokemonInBag pokemonA = new PokemonInBag("Ultra Ball");
        PokemonInBag pokemonB = new PokemonInBag("Special Ball");
        
        var initialExtent = PokemonInBag.GetPokemonsInBag();
        Assert.That(initialExtent.Count, Is.EqualTo(2));
        
        PokemonInBag.save(TestPath);
        
        Assert.IsTrue(File.Exists(TestPath), "File should be created in bin folder");
        
        bool loadSuccess = PokemonInBag.load(TestPath);
        
        Assert.IsTrue(loadSuccess, "Load should return true");
        
        var loadedExtent = PokemonInBag.GetPokemonsInBag();
        
        var loadedPokemonA = loadedExtent.OfType<PokemonInBag>().FirstOrDefault(p => p.Pokeball == "Ultra Ball");
        
        Assert.IsNotNull(loadedPokemonA, "Shop should be retrieved");
        Assert.That(loadedPokemonA.Pokeball, Is.EqualTo("Ultra Ball"));
    }
}