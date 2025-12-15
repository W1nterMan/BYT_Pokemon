namespace Models;

[Serializable]
public class Water : Pokemon
{
    public bool CanSwim { get; set; }

    public Water(){}
    public Water(int id, string name, int healthPoints, int expPoints, double weight, int[] baseStats,Nature nature, bool canSwim) :
        base(id, name, healthPoints, expPoints, weight, baseStats, nature)
    {
        CanSwim = canSwim;
    }
}