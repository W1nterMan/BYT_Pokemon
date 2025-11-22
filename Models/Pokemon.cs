namespace Models;

[Serializable]
public class Pokemon
{
    private static List<Pokemon> _extent=new List<Pokemon>();
    private int _healthPoints;
    private string _name;
    private int _expPoints;
    private double _weight;
    private int[] _baseStats = new int[6]; // <-- [hp,attack,defence,sp.atk,sp.def,speed]
    private string? _status; //can be "","Active","Defeated"
    
    public int Id { get; set; }
    public int HealthPoints
    {
        get =>_healthPoints;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("HealthPoints must be greater than 0");
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
                !(value.Equals("Active") || value.Equals("Defeated")))
            {
                throw new ArgumentException("Status must not be null or empty");
            }

            _status = value;
        }
    } 

    public Pokemon? EvolvesTo { get; set; }

    public PokemonInBag? PokemonInBag { get; set; } 
    
    public Pokemon(int id, string name, int healthPoints, int expPoints, double weight, int[] baseStats)
    {
        //constructor...
        Id = id;
        Name = name;
        HealthPoints = healthPoints;
        ExpPoints = expPoints;
        Weight = weight;
        BaseStats = baseStats;
    }

    static void AddPokemon(Pokemon pokemon)
    {
        if (pokemon == null)
        {
            throw new ArgumentNullException("Pokemon must not be null");
        }
        _extent.Add(pokemon);
    }

    static List<Pokemon> GetPokemons()
    {
        return _extent;
    }
    
}