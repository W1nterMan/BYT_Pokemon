namespace Models;

[Serializable]
public class Land 
{
    private static List<Land> _extent=new List<Land>();
    private Pokemon _pokemon;
    private int _autoHealPoint;
    
    public Pokemon Pokemon => _pokemon;

    public int AutoHealPoint => _autoHealPoint;
    
    public Land(){}
    
    public Land(Pokemon pokemon, int autoHealPoint)
    {
        _pokemon = pokemon;
        if (autoHealPoint < 0)
        {
            throw new ArgumentException("Auto heal point cannot be negative");
        }
        _autoHealPoint  =  autoHealPoint;
        _extent.Add(this);
    }

    public List<Land> GetExtent() => new List<Land>(_extent);
    
    public static void  RemoveFromExtent(Land land) => _extent.Remove(land);
}