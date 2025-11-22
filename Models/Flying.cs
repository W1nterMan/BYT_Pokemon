namespace Models;

[Serializable]
public class Flying : Pokemon
{
    public bool CanFly { get; set; }

    public Flying(int id, string name, int healthPoints, int expPoints, double weight, int[] baseStats, bool canFly) :
        base(id, name, healthPoints, expPoints, weight, baseStats)
    {
        CanFly =  canFly;
    }
}