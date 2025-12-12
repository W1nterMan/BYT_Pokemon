using System.Reflection;
using Models;

namespace Test;

public class PokemonTest
{
    [SetUp]
    public void Setup()
    {
        var field =typeof(Pokemon).GetField("_extent", BindingFlags.Static|BindingFlags.NonPublic);
        field.SetValue(null, new List<Pokemon>());
    }

    private static Nature _nature = new Nature("Brave", 1, 2);
    private static object[] _testcases =
    {
        new object[] { 0, "pokemonA", 100, 100, 100, new[] { 1, 1, 1, 1, 1, 1 }, _nature },
        new object[] { 1, "", 100, 100, 100, new[] { 1, 1, 1, 1, 1, 1 }, _nature },
        new object[] { 1, "pokemonA", -1, 100, 100, new[] { 1, 1, 1, 1, 1, 1 }, _nature },
        new object[] { 1, "pokemonA", 100, -1, 100, new[] { 1, 1, 1, 1, 1, 1 }, _nature },
        new object[] { 1, "pokemonA", 100, 100, 0, new[] { 1, 1, 1, 1, 1, 1 }, _nature },
        new object[] { 1, "pokemonA", 100, 100, 100, new[] { 1, 1, 1, 1, 1 }, _nature },
    };
    
    /*[TestCase(0,"pokemonA",100,100,100,new []{1,1,1,1,1,1}, _nature)]
    [TestCase(1,"",100,100,100,new []{1,1,1,1,1,1},new Nature("Brave",1,2))]
    [TestCase(1,"pokemonA",-1,100,100,new []{1,1,1,1,1,1})]
    [TestCase(1,"pokemonA",100,-1,100,new []{1,1,1,1,1,1})]
    [TestCase(1,"pokemonA",100,100,0,new []{1,1,1,1,1,1})]
    [TestCase(1,"pokemonA",100,100,100,new []{1,1,1,1,1})]*/
   [TestCaseSource(nameof(_testcases))]
    public void Pokemon_Invalid_Argument_ThrowException
    (int id, string name,int healthPoints,
        int expPoints,double weight,int[] baseStats,Nature nature)
    {
        Assert.Throws<ArgumentException>(()=>new Pokemon(id,name,healthPoints,expPoints,weight,baseStats,nature));
    }
    
    /*[Test]
    public void Pokemon_Nullable_EvolvesTo_Test()
    {
        Pokemon pokemonA=new Pokemon(
            1,"pokemonA",100,100,100,[1,1,1,1,1,1]);
        Pokemon pokemonB=new Pokemon(
            1,"pokemonB",100,100,100,[1,1,1,1,1,1]);
        Assert.IsNull(pokemonA.EvolvesTo);
        pokemonA.EvolvesTo = pokemonB;
        Assert.IsNotNull(pokemonA.EvolvesTo);
    }*/

    [Test]
    public void Pokemon_Nullable_Status_Test()
    {
        Pokemon pokemonA=new Pokemon(
            1,"pokemonA",100,100,100,[1,1,1,1,1,1], _nature);
        Pokemon pokemonB=new Pokemon(
            1,"pokemonB",100,100,100,[1,1,1,1,1,1], _nature);
        Assert.IsNull(pokemonA.Status);
        Assert.IsNull(pokemonB.Status);
        pokemonA.Status = nameof(StatusEnum.Active);
        Assert.IsNotNull(pokemonA.Status);
        Assert.Throws<ArgumentException>(() => pokemonB.Status = "something else");
    }

    [Test]
    public void Fire_Pokemon_Valid_And_Invalid_Test()
    {
        Fire fireValid=new Fire(
            1,"fireA",100,100,100,[1,1,1,1,1,1], _nature, 100);
        Assert.Throws<ArgumentException>(() => 
            new Fire(
                1, "fireB", 100, 100, 100, [1, 1, 1, 1, 1, 1], _nature, 0));
        Assert.That(fireValid.BodyTemperature, Is.EqualTo(100));
    }
    
    [Test]
    public void Land_Pokemon_Valid_And_Invalid_Test()
    {
        Land landValid=new Land(
            1,"landA",100,100,100,[1,1,1,1,1,1], _nature,100);
        Assert.Throws<ArgumentException>(() => 
            new Land(
                1, "fireB", 100, 100, 100, [1, 1, 1, 1, 1, 1], _nature, -1));
        Assert.That(landValid.AutoHealPoint,Is.EqualTo(100));
    }
    
    [Test]
    public void Underwater_Pokemon_StaticAttribute_IsShared()
    {
        Underwater pokemon1=new Underwater(
            1,"underwaterA",100,100,100,[1,1,1,1,1,1], _nature);
        pokemon1.ExpPoints = (int)(Underwater.ExpBonusRate*pokemon1.ExpPoints);
        Assert.That(pokemon1.ExpPoints,Is.EqualTo(110));
    }

    [Test]
    public void Pokemon_Extent_Test()
    {
        Fire firePokemon = new Fire(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _nature, 100);
        Water waterPokemon = new Water(
            1, "waterA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _nature, true);

        var extent = Pokemon.GetPokemons();
        Assert.That(extent.Count, Is.EqualTo(2));
        Assert.That(extent.Find(p=>p.Name=="fireA").Name, Is.EqualTo("fireA"));
        Assert.That(extent.Find(p=>p.Name=="waterA").Name, Is.EqualTo("waterA"));
    }

    [Test]
    public void Pokemon_Encapsulation_Test()
    {
        Fire firePokemon = new Fire(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _nature, 100);
        Water waterPokemon = new Water(
            1, "waterA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _nature, true);

        var extent = Pokemon.GetPokemons();
        Assert.That(extent.Find(p=>p.Name=="fireA").Name, Is.EqualTo("fireA"));
        Assert.That(extent.Find(p=>p.Name=="waterA").Name, Is.EqualTo("waterA"));
        
        firePokemon.Name = "fireB";
        waterPokemon.Name = "waterB";
        
        Assert.That(extent.Find(p=>p.Name=="fireB").Name, Is.EqualTo("fireB"));
        Assert.That(extent.Find(p=>p.Name=="waterB").Name, Is.EqualTo("waterB"));
    }

    [Test]
    public void Pokemon_Persistence_Test()
    {
        string TestPath = "test_pokemons.xml";
        
        if (File.Exists(TestPath)) File.Delete(TestPath);

        Land land = new Land(
            1, "landA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _nature, 100);
        
        Fire fire = new Fire(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _nature, 100);
        
        var initialExtent = Pokemon.GetPokemons();
        Assert.That(initialExtent.Count, Is.EqualTo(2));
        
        Pokemon.Save(TestPath);
        
        bool loadSuccess = Pokemon.Load(TestPath);

        Assert.IsTrue(loadSuccess, "Load should return true");
        
        var loadedExtent = Pokemon.GetPokemons();
        
        var loadedLand = loadedExtent.OfType<Land>().FirstOrDefault(l => l.Name == "landA");
        
        Assert.IsNotNull(loadedLand, "Shop should be retrieved");
        Assert.That(loadedLand.AutoHealPoint, Is.EqualTo(100));
        
        var loadedFire = loadedExtent.OfType<Fire>().FirstOrDefault(f => f.Name == "fireA");
        Assert.IsNotNull(loadedFire, "Gym should be retrieved");
        Assert.That(loadedFire.BodyTemperature, Is.EqualTo(100));
    }
    
}