namespace Models;

//[Serializable]
public class Bag
{
    private static List<Bag> _extent = new List<Bag>();

    //Attributes
    private static int _maximumQuantity = 6;

    public static int MaximumQuantity
    {
        get => _maximumQuantity;
        set
        {
            if (value < 0) throw new ArgumentException("Quantity cannot be less than zero.");
            _maximumQuantity = MaximumQuantity;
        }
    }

    //Associations

    private HashSet<PokemonInBag> _pokemonsInBag = new HashSet<PokemonInBag>();

    private Trainer _owner;

    public Trainer Owner
    {
        get => _owner;
        set
        {
            if (value == null) throw new ArgumentNullException("Owner cannot be null");
            _owner = value;
        }
    }

    public Bag(Trainer owner)
    {
        Owner = owner;
        owner.Bag = this;

        _extent.Add(this);
    }

    public HashSet<PokemonInBag> OpenBag() => new HashSet<PokemonInBag>(_pokemonsInBag);

    public void StorePokemon(PokemonInBag pokemon)
    {
        if (pokemon == null) throw new ArgumentNullException(nameof(pokemon));
        if (_pokemonsInBag.Count >= 6) throw new InvalidOperationException("Invalid bag configuration.");
        _pokemonsInBag.Add(pokemon);
    }

    public PokemonInBag TakePokemon(PokemonInBag pokemon)
    {
        if (pokemon == null) throw new ArgumentNullException(nameof(pokemon));
        if (!_pokemonsInBag.Contains(pokemon)) throw new InvalidOperationException("Pokemon not in bag.");
        _pokemonsInBag.Remove(pokemon);
        return pokemon;
    }

    public static void RemoveFromExtent(Bag bag) => _extent.Remove(bag);

    public static void Save(string path = "bags.xml")
    {
        Serializer.Save(path, _extent);
    }

    public static bool Load(string path = "bags.xml")
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