namespace Models;

[Serializable]
public class Fire : Pokemon
{
    private double _bodyTemperature;

    public double BodyTemperature
    {
        get => _bodyTemperature;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("BodyTemperature must be greater than zero");
            }
            _bodyTemperature = value;
        }
    }
    
    public Fire(){}

    public Fire(int id, string name, int healthPoints, int expPoints, double weight, int[] baseStats,Nature nature, double bodyTemperature) :
        base(id, name, healthPoints, expPoints, weight, baseStats,nature)
    {
       BodyTemperature  =  bodyTemperature;
    }
}