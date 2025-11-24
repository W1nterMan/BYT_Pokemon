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

    //public Pokemon? EvolvesTo { get; set; }
    
    //comment out at current step
    //public PokemonInBag? PokemonInBag { get; set; }
    
    public Pokemon(){}
    
    public Pokemon(int id, string name, int healthPoints, int expPoints, double weight, int[] baseStats)
    {
        //constructor...
        Id = id;
        Name = name;
        HealthPoints = healthPoints;
        ExpPoints = expPoints;
        Weight = weight;
        BaseStats = baseStats;
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

    public static void save(string path = "pokemons.xml")
    {
        StreamWriter file = File.CreateText(path);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Pokemon>)); 
        using (XmlTextWriter writer = new XmlTextWriter(file)) 
        {
            xmlSerializer.Serialize(writer, _extent); 
        }
    }
    
    public static bool load(string path = "pokemons.xml")
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

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Pokemon>));
        using (XmlTextReader reader = new XmlTextReader(file))
        {
            try
            {
                _extent = (List<Pokemon>)xmlSerializer.Deserialize(reader);
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

public enum StatusEnum
{
Active,
Defeated
}