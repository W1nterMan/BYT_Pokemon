using System.Xml;
using System.Xml.Serialization;

namespace Models;

[Serializable]
public class PokemonInBag
{
    private static List<PokemonInBag> _extent=new List<PokemonInBag>();
    private Pokemon _pokemon;
    private Bag _bag;
    private string _pokeball;
    public Pokemon Pokemon
    {
        get => _pokemon;
        set
        {
            _pokemon = value ?? throw new ArgumentException("Pokemon can not be null");
        }
    }

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

    public Bag Bag
    {
        get => _bag;
        set
        {
            _bag = value ?? throw new ArgumentException("Bag can not be null");
        }
    }
    
    public PokemonInBag(){}

    public PokemonInBag( Pokemon pokemon,Bag bag, string pokeball)
    {
        Pokemon = pokemon;
        Pokeball = pokeball;
        Bag = bag;
        bag.StorePokemon(this);
        pokemon.AddPokemonToBag(this);
        AddPokemonInBag(this);
    }

    public void RemovePokemonFromBag()
    {
        _pokemon.RemoveFromBag(this);
        _bag.TakePokemon(this);
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
    
    
    public static void Save(string path = "pokemons_in_bag.xml")
    {
        StreamWriter file = File.CreateText(path);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PokemonInBag>)); 
        using (XmlTextWriter writer = new XmlTextWriter(file)) 
        {
            xmlSerializer.Serialize(writer, _extent); 
        }
    }
    
    public static bool Load(string path = "pokemons_in_bag.xml")
    {
        StreamReader file;
        try
        {
            file = File.OpenText(path);
        }
        catch (FileNotFoundException)
        {
            _extent.Clear();
            return false;    
        }

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<PokemonInBag>));
        using (XmlTextReader reader = new XmlTextReader(file))
        {
            try
            {
                _extent = (List<PokemonInBag>)xmlSerializer.Deserialize(reader);
            }
            catch (InvalidCastException)
            {
                _extent.Clear();
                return false;   
            }
            catch (Exception)
            {
                _extent.Clear();
                return false;   
            }
        }
        return true; 
    }
}