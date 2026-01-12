namespace Models;

[Serializable]
public class Water : Pokemon
{
    private static List<Water> _extent = new List<Water>();
    private Pokemon _pokemon;
    public Pokemon Pokemon => _pokemon;
    private bool _canSwim;
    public bool CanSwim  => _canSwim;

    public Water(){}
    public Water(Pokemon pokemon, bool canSwim) 
    {
        _pokemon = pokemon;
        _canSwim = canSwim;
        _extent.Add(this);
    }
    
    public List<Water> GetExtent() => new List<Water>(_extent);
    public static void RemoveFromExtent(Water water) =>  _extent.Remove(water);
}