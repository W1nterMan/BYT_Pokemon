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
        new object[] { 0, "pokemonA", 100, 100, 100, new[] { 1, 1, 1, 1, 1, 1 }, _nature, 30},
        new object[] { 1, "", 100, 100, 100, new[] { 1, 1, 1, 1, 1, 1 }, _nature, 30 },
        new object[] { 1, "pokemonA", -1, 100, 100, new[] { 1, 1, 1, 1, 1, 1 }, _nature, 30 },
        new object[] { 1, "pokemonA", 100, -1, 100, new[] { 1, 1, 1, 1, 1, 1 }, _nature, 30 },
        new object[] { 1, "pokemonA", 100, 100, 0, new[] { 1, 1, 1, 1, 1, 1 }, _nature, 30 },
        new object[] { 1, "pokemonA", 100, 100, 100, new[] { 1, 1, 1, 1, 1 }, _nature, 30 },
    };
    
   [TestCaseSource(nameof(_testcases))]
    public void Pokemon_Invalid_Argument_ThrowException
    (int id, string name,int healthPoints,
        int expPoints,double weight,int[] baseStats,Nature nature, double bodyTemperature)
    {
        Assert.Throws<ArgumentException>(()=>new PokemonBuilder(id,name,healthPoints,expPoints,weight,baseStats,nature)
                                                                .FireType(bodyTemperature)
                                                                .LandEggType(10)
                                                                .Build());
    }
    
    [Test]
    public void Pokemon_Nullable_Status_Test()
    {
        Pokemon pokemonA=new PokemonBuilder(1, "pokemonA", 20, 1, 40, new int[]{1,1,1,1,1,1}, _nature)
                                            .FireType(30)
                                            .LandEggType(10)
                                            .Build();
        Pokemon pokemonB=new PokemonBuilder(2, "pokemonB", 20, 1, 40, new int[]{1,1,1,1,1,1}, _nature)
                                            .FireType(30)
                                            .LandEggType(10)
                                            .Build();
        Assert.IsNull(pokemonA.Status);
        Assert.IsNull(pokemonB.Status);
        pokemonA.Status = nameof(StatusEnum.Active);
        Assert.IsNotNull(pokemonA.Status);
        Assert.Throws<ArgumentException>(() => pokemonB.Status = "something else");
    }

    [Test]
    public void Fire_Pokemon_Valid_And_Invalid_Test()
    {
        Pokemon fireValid=new PokemonBuilder(
            1,"fireA",100,100,100,[1,1,1,1,1,1], _nature)
            .FireType(100)
            .LandEggType(10)
            .Build();
        Assert.Throws<ArgumentException>(() => 
            new PokemonBuilder(
                2, "fireB", 100, 100, 100, [1, 1, 1, 1, 1, 1], _nature)
                .FireType(0)
                .LandEggType(10)
                .Build());
        Assert.That(fireValid.Fire.BodyTemperature, Is.EqualTo(100));
    }
    
    //remove egg type instantiation test
    [Test]
    public void Land_Pokemon_Valid_And_Invalid_Test()
    {
        Pokemon landValid=new PokemonBuilder(
            1,"landA",100,100,100,[1,1,1,1,1,1], _nature)
            .FireType(30).LandEggType(100).Build();
        Assert.Throws<ArgumentException>(() => 
            new PokemonBuilder(
                1, "fireB", 100, 100, 100, [1, 1, 1, 1, 1, 1], _nature)
                .FireType(100)
                .LandEggType(-1)
                .Build());
        Assert.That(landValid.Land.AutoHealPoint,Is.EqualTo(100));
    }
    
    [Test]
    public void Underwater_Pokemon_StaticAttribute_IsShared()
    {
        Pokemon pokemon1=new PokemonBuilder(
            1,"underwaterA",100,100,100,[1,1,1,1,1,1], _nature)
            .WaterType(true)
            .UnderwaterEggType()
            .Build();
        pokemon1.ExpPoints = (int)(Underwater.ExpBonusRate*pokemon1.ExpPoints);
        Assert.That(pokemon1.ExpPoints,Is.EqualTo(110));
    }

    [Test]
    public void Pokemon_Extent_Test()
    {
        Pokemon firePokemon = new PokemonBuilder(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _nature)
            .FireType(100)
            .LandEggType(10)
            .Build();
        Pokemon waterPokemon = new PokemonBuilder(
            2, "waterA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _nature)
            .WaterType(true)
            .UnderwaterEggType()
            .Build();

        var extent = Pokemon.GetPokemons();
        Assert.That(extent.Count, Is.EqualTo(2));
        Assert.That(extent.Find(p=>p.Name=="fireA").Name, Is.EqualTo("fireA"));
        Assert.That(extent.Find(p=>p.Name=="waterA").Name, Is.EqualTo("waterA"));
    }

    [Test]
    public void Pokemon_Encapsulation_Test()
    {
        Pokemon firePokemon = new PokemonBuilder(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _nature)
            .FireType(100)
            .LandEggType(10)
            .Build();
        Pokemon waterPokemon = new PokemonBuilder(
            2, "waterA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _nature)
            .WaterType(true)
            .UnderwaterEggType()
            .Build();

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

        Pokemon firePokemon = new PokemonBuilder(
            1, "fireA", 100, 100, 100, [1, 1, 1, 1, 1, 1], _nature)
            .FireType(100)
            .LandEggType(10)
            .Build();
        Pokemon waterPokemon = new PokemonBuilder(
            2, "waterA", 100, 100, 200, [1, 1, 1, 1, 1, 1], _nature)
            .WaterType(true)
            .UnderwaterEggType()
            .Build();
        
        var initialExtent = Pokemon.GetPokemons();
        Assert.That(initialExtent.Count, Is.EqualTo(2));
        
        Pokemon.Save(TestPath);
        
        bool loadSuccess = Pokemon.Load(TestPath);

        Assert.IsTrue(loadSuccess, "Load should return true");
        
        var loadedExtent = Pokemon.GetPokemons();
        
        var loadedWater = loadedExtent.OfType<Pokemon>().FirstOrDefault(l => l.Name == "waterA");
        
        Assert.IsNotNull(loadedWater, "Water pokemon should be retrieved");
        Assert.That(loadedWater.Weight, Is.EqualTo(200));
        
        var loadedFire = loadedExtent.OfType<Pokemon>().FirstOrDefault(f => f.Name == "fireA");
        Assert.IsNotNull(loadedFire, "Fire pokemon should be retrieved");
        Assert.That(loadedFire.Weight, Is.EqualTo(100));
    }
    
}