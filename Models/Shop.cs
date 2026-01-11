using System.Xml.Serialization;

namespace Models
{
    [Serializable]
    public class Shop
    {
        private static List<Shop> _extent = new List<Shop>();
        [XmlIgnore]
        private Building _building;
        private double _priceMultiplier;
        
        public Building Building => _building;
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

        public Shop(Building building, double multiplier)
        {
            _building = building;
            PriceMultiplier = multiplier;
            _extent.Add(this);
        }

        public void AddItem(string item)
        {
            if (string.IsNullOrEmpty(item)) throw new ArgumentException("Item name cannot be empty."); 
            ItemsSold.Add(item);
        }

        public static List<Shop> GetExtent() => new List<Shop>(_extent);
        public static void RemoveFromExtent(Shop shop) => _extent.Remove(shop);
    }
}