

namespace Models
{
    [Serializable]
    public class Pokecenter : Building
    {
        private static double _baseHealingCost = 0; 

        public Pokecenter() { }

        public Pokecenter(string name, bool isAccessible) : base(name, isAccessible)
        {
        }
        
        public static double BaseHealingCost
        {
            get => _baseHealingCost;
            set
            {
                if (value < 0) throw new ArgumentException("Healing cost cannot be negative.");
                _baseHealingCost = value;
            }
        }
    }
}