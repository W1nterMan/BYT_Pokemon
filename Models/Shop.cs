

namespace Models
{
    [Serializable]
    public class Shop : Building
    {
        private double _priceMultiplier;
        
        public List<string> ItemsSold { get; set; } = new List<string>(); //dont know for sure if this would be complex in future

        public double PriceMultiplier
        {
            get => _priceMultiplier;
            set
            {
                if (value <= 0) throw new ArgumentException("Multiplier must be greater than 0.");
                _priceMultiplier = value;
            }
        }

        public Shop() { }

        public Shop(string name, bool isAccessible, double multiplier) : base(name, isAccessible)
        {
            PriceMultiplier = multiplier;
        }

        public void AddItem(string item)
        {
            if (string.IsNullOrEmpty(item)) throw new ArgumentException("Item name cannot be empty."); 
            ItemsSold.Add(item);
        }
    }
}