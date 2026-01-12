namespace Models;

[Serializable]
public class Fire
{
    private static List<Fire> _extent = new List<Fire>();
    private Pokemon _pokemon;
    public Pokemon Pokemon => _pokemon;
    private double  _bodyTemperature;

    public double BodyTemperature => _bodyTemperature;
    
    public Fire(){}

    public Fire(Pokemon pokemon,double bodyTemperature)
    {
        _pokemon = pokemon;
        
        if (bodyTemperature <= 0)
        {
            throw new ArgumentException("BodyTemperature must be greater than zero");
        }
        
        _bodyTemperature  =  bodyTemperature;
        
        _extent.Add(this);
    }

    public List<Fire> GetExtent() => new List<Fire>(_extent);
    public static void RemoveFromExtent(Fire fire)=>_extent.Remove(fire);
}