namespace Models;

[Serializable]
public class Underwater (int id, string name, int healthPoints, int expPoints, double weight, int[] baseStats) :
Pokemon(id, name, healthPoints, expPoints, weight, baseStats)
{
    public static double ExpBonusRate { get; } = 1.1;
}