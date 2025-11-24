namespace Models;

[Serializable]
public class Water : Pokemon
{
    public bool CanSwim { get; set; }

    public Water(){}
    public Water(int id, string name, int healthPoints, int expPoints, double weight, int[] baseStats, bool canSwim) :
        base(id, name, healthPoints, expPoints, weight, baseStats)
    {
        CanSwim = canSwim;
    }
}