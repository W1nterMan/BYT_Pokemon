using System.Xml;
using System.Xml.Serialization;

namespace Models;

[Serializable]
public class Nature
{
    private static List<Nature> _extent=new List<Nature>();
    private string _name;
    private int _raisedStat; //0~5, according to index of _baseStats of Pokemon
    private int _loweredStat; //same
    public static double StatsBoostRate { get;} = 0.1;
    private HashSet<Pokemon> _pokemons = new HashSet<Pokemon>();

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Name cannot be empty");
            }

            _name = value;
        }
    }
    public int RaisedStat
    {
        get =>_raisedStat;
        set
        {
            if (value < 0 || value > 5)
            {
                throw new ArgumentException("Raised stat must be between 0 and 5");
            }
            
            _raisedStat = value;
        }
    }
    
    public int LoweredStat
    {
        get =>_loweredStat;
        set
        {
            if (value < 0 || value > 5)
            {
                throw new ArgumentException("Lowered stat must be between 0 and 5");
            }
            _loweredStat = value;
        }
    }
    
    public Nature(){}

    public Nature(string name,int raisedStat, int loweredStat)
    {
        Name = name;
        RaisedStat = raisedStat;
        LoweredStat = loweredStat;
        AddNature(this);
    }
    
    private static void AddNature(Nature nature)
    {
        if (nature == null)
        {
            throw new ArgumentException("Nature can not be null");
        }
        if(nature.RaisedStat == nature.LoweredStat)
            throw new ArgumentException("Raised stat must be different than the lowered stat");
        if (_extent.Exists(n => n.Name == nature.Name))
        {
            throw new ArgumentException(nature.Name+" already exists");
        }
        _extent.Add(nature);
    }

    public static List<Nature> GetNatures()
    {
        return _extent;
    }

    public static void Save(string path = "natures.xml")
    {
        StreamWriter file = File.CreateText(path);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Nature>)); 
        using (XmlTextWriter writer = new XmlTextWriter(file)) 
        {
            xmlSerializer.Serialize(writer, _extent); 
        }
    }
    
    public static bool Load(string path = "natures.xml")
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

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Nature>));
        using (XmlTextReader reader = new XmlTextReader(file))
        {
            try
            {
                _extent = (List<Nature>)xmlSerializer.Deserialize(reader);
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
    
    public HashSet<Pokemon> GetNaturePokemons()=>new HashSet<Pokemon>(_pokemons);
    
    public void AddPokemon(Pokemon pokemon)
    {
        if (_pokemons.Contains(pokemon))
        {
            return;
        }
            
        bool added = false;
            
        try
        {
            _pokemons.Add(pokemon);
            added = true;
        }
        catch (Exception e)
        {
            if (added)
            {
                _pokemons.Remove(pokemon);
            }
        }
    }

    public void RemovePokemon(Pokemon pokemon)
    {
        if (!_pokemons.Contains(pokemon))
        {
            return;
        }
        
        bool removed = false;
            
        try
        {
            _pokemons.Remove(pokemon);
            removed = true;
        }
        catch (Exception e)
        {
            if (removed)
            {
                _pokemons.Add(pokemon);
            }
        }
    }
}