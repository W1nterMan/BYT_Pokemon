namespace Models;

[Serializable]
public class Land : Pokemon
{
    private int _autoHealPoint;

    public int AutoHealPoint
    {
        get => _autoHealPoint;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Auto heal point cannot be negative");
            }
            _autoHealPoint = value;
        }
    }

    public Land(){}
    
    public Land(int id, string name, int healthPoints, int expPoints, double weight, int[] baseStats,Nature nature, int autoHealPoint) :
        base(id, name, healthPoints, expPoints, weight, baseStats,nature)
    {
        AutoHealPoint  =  autoHealPoint;
    }
}