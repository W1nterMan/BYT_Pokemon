using System.Xml;
using System.Xml.Serialization;

namespace Models;

[Serializable]
public class PokemonInBag
{
    private static List<PokemonInBag> _extent = new List<PokemonInBag>();
    
    //Attributes
    private string _pokeball;
    
    public string Pokeball
    {
        get => _pokeball;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Pokeball can not be null or empty");
            }
            _pokeball = value;
        }
    }
    
    //Associations
    private Pokemon _pokemon;
    private Bag _bag;
   
    public Pokemon Pokemon
    {
        get => _pokemon;
        set
        {
            _pokemon = value ?? throw new ArgumentException("Pokemon can not be null");
        }
    }
    
    public Bag Bag
    {
        get => _bag;
        set
        {
            _bag = value ?? throw new ArgumentException("Bag can not be null");
        }
    }

    public void RemovePokemonFromBag()
    {
        _pokemon.RemovePokemonFromBag(this);
        _bag.TakePokemon(this);

        _extent.Remove(this);
    }
    
    private static void AddPokemonInBag(PokemonInBag pokemonInBag)
    {
        if (pokemonInBag == null)
        {
            throw new ArgumentException("Pokemon can not be null in bag");
        }
        _extent.Add(pokemonInBag);
    }

    public static List<PokemonInBag> GetPokemonsInBag()
    {
        return _extent;
    }
    
    public PokemonInBag(){}

    public PokemonInBag( Pokemon pokemon,Bag bag, string pokeball)
    {
        Pokeball = pokeball;
        
        Pokemon = pokemon;
        Bag = bag;
        bag.StorePokemon(this);
        pokemon.AddPokemonToBag(this);
        
        AddPokemonInBag(this);
    }
    
    public static void Save(string path = "pokemons_in_bag.xml")
    {
        Serializer.Save(path, _extent);
    }
    
    public static bool Load(string path = "pokemons_in_bag.xml")
    {
        var loadedList = Serializer.Load(path, _extent);
        
        if (loadedList != null)
        {
            _extent = loadedList;
            return true;
        }
        return false;
    }
}