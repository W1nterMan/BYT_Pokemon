namespace Models;

[Serializable]
public class Underwater : Pokemon
{
    public static double ExpBonusRate { get; } = 1.1;
    public Underwater(){}

    public Underwater(int id, string name, int healthPoints, int expPoints, double weight, int[] baseStats, Nature nature) :
        base(id, name, healthPoints, expPoints, weight, baseStats,nature)
    { }
}