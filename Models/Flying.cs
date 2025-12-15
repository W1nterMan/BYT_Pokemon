namespace Models;

[Serializable]
public class Flying : Pokemon
{
    public bool CanFly { get; set; }

    public Flying(){}
    public Flying(int id, string name, int healthPoints, int expPoints, double weight, int[] baseStats,Nature nature, bool canFly) :
        base(id, name, healthPoints, expPoints, weight, baseStats,nature)
    {
        CanFly =  canFly;
    }
}