

namespace Models
{
    [Serializable]
    public class Shop : Building
    {
        private double _priceMultiplier;
        
        public List<string> ItemsSold { get; set; } = new List<string>(); //dont know for sure if this would be complex in future -> Yes most likely it will but
                                                                          //for the time being we dont have item class specified in the diagram and have no idea what is needed 

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

        public Shop(string name, bool isAccessible, double multiplier, Location location) : base(name, isAccessible, location)
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