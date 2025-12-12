using System.Xml;
using System.Xml.Serialization;

namespace Models;

[Serializable]
[XmlInclude(typeof(Fire))]
[XmlInclude(typeof(Flying))]
[XmlInclude(typeof(Water))]
[XmlInclude(typeof(Land))]
[XmlInclude(typeof(Underwater))]
public class Pokemon
{
    private static List<Pokemon> _extent=new List<Pokemon>();
    private int _id;
    private int _healthPoints;
    private string _name;
    private int _expPoints;
    private double _weight;
    private int[] _baseStats = new int[6]; // <-- [hp,attack,defence,sp.atk,sp.def,speed]
    private string? _status; //can be "","Active","Defeated"
    private Pokemon? _evolvesTo;
    private HashSet<PokemonInBag>  _pokemonsInBag=new HashSet<PokemonInBag>();

    public int Id
    {
        get => _id;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Id must be greater than 0");
            }
            _id = value;
        }
    }

    public int HealthPoints
    {
        get =>_healthPoints;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("HealthPoints cannot be negative");
            }
            
            _healthPoints= value;
        }
    }
    public string Name { 
        get=>_name;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Name must not be null or empty");
            }
            _name = value;
        } }
    public int ExpPoints { get=>_expPoints;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("ExpPoints must be non-negative");
            }
            _expPoints = value;
        } }
    public double Weight { get=>_weight;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Weight must be greater than 0");
            }
            _weight = value;
        } }

    public int[] BaseStats
    {
        get => _baseStats;
        set
        {
            if (value.Length != 6)
            {
                throw new ArgumentException("BaseStats must have 6 values");
            }

            foreach (var stat in  value)
            {
                if (stat < 0)
                {
                    throw new ArgumentException("BaseStats must be non-negative");
                }
            }
            _baseStats = value;
        }
    }

    public string? Status
    {
        get => _status;
        set
        {
            if (!string.IsNullOrEmpty(value) &&
                !(value.Equals(nameof(StatusEnum.Active)) || value.Equals(nameof(StatusEnum.Defeated))))
            {
                throw new ArgumentException("Status must not be null or empty");
            }

            _status = value;
        }
    }

    public Pokemon? EvolvesTo
    {
        get => _evolvesTo;
        set
        {
            if (value != null && value._id == _id)
            {
                throw new ArgumentException("Same pokemon cannot be specified to evolution target");
            }
            _evolvesTo = value;
        }
    }
    
    public Nature Nature { get;  }
    
    public Pokemon(){}
    
    public Pokemon(int id, string name, int healthPoints, int expPoints, double weight, int[] baseStats, Nature nature)
    {
        //constructor...
        Id = id;
        Name = name;
        HealthPoints = healthPoints;
        ExpPoints = expPoints;
        Weight = weight;
        BaseStats = baseStats;
        Nature=nature;
        nature.AddPokemon(this);
        AddPokemon(this);
    }

    private static void AddPokemon(Pokemon pokemon)
    {
        if (pokemon == null)
        {
            throw new ArgumentException("Pokemon can not be null");
        }
        _extent.Add(pokemon);
    }

    public static List<Pokemon> GetPokemons()
    {
        return _extent;
    }

    public static void Save(string path = "pokemons.xml")
    {
        Serializer.Save(path, _extent);
    }

    public static bool Load(string path = "pokemons.xml")
    {
        var loadedList = Serializer.Load(path, _extent);
        
        if (loadedList != null)
        {
            _extent = loadedList;
            return true;
        }
        return false;
    }
    
    public HashSet<PokemonInBag> GetPokemonsInBag()=>new HashSet<PokemonInBag>(_pokemonsInBag);

    public void AddPokemonToBag(PokemonInBag pokemonInBag)
    {
        if (_pokemonsInBag.Contains(pokemonInBag))
        {
            return;
        }
            
        bool added = false;
            
        try
        {
            _pokemonsInBag.Add(pokemonInBag);
            added = true;
        }
        catch (Exception e)
        {
            if (added)
            {
                _pokemonsInBag.Remove(pokemonInBag);
            }
        }
    }
    
    public void RemoveFromBag(PokemonInBag pokemonInBag)
    {
        if (!_pokemonsInBag.Contains(pokemonInBag))
        {
            return;
        }
        
        bool removed = false;
            
        try
        {
            _pokemonsInBag.Remove(pokemonInBag);
            removed = true;
        }
        catch (Exception e)
        {
            if (removed)
            {
                _pokemonsInBag.Add(pokemonInBag);
            }
        }
    }
    
}

public enum StatusEnum
{
Active,
Defeated
}