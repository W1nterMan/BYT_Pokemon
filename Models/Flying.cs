namespace Models;

[Serializable]
public class Flying 
{
    private static List<Flying> _extent=new List<Flying>();
    private Pokemon _pokemon;
    public Pokemon Pokemon => _pokemon;
    private bool _canFly;
    public bool CanFly => _canFly;
    
    public Flying(){}
    public Flying(Pokemon pokemon, bool canFly) 
    {
        _pokemon = pokemon;
        _canFly =  canFly;
        _extent.Add(this);
    }
    
    public List<Flying> GetExtent() => new List<Flying>(_extent);
    public static void RemoveFromExtent(Flying flying)=>_extent.Remove(flying);
}