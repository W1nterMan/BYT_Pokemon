namespace Models;

[Serializable]
public class Underwater
{
    private static List<Underwater> _extent = new List<Underwater>();
    private Pokemon _pokemon;
    public Pokemon pokemon => _pokemon;
    public static double ExpBonusRate { get; } = 1.1;
    public Underwater(){}

    public Underwater(Pokemon pokemon)
    {
        _pokemon = pokemon;
        _extent.Add(this);
    }
    
    public List<Underwater> GetExtent() => new List<Underwater>(_extent);
    public static void RemoveFromExtent(Underwater underwater)=>_extent.Remove(underwater);
}