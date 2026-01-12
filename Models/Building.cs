using System.Xml;
using System.Xml.Serialization;

namespace Models
{
    [Serializable]
    [XmlInclude(typeof(Shop))]
    [XmlInclude(typeof(Gym))]
    [XmlInclude(typeof(Pokecenter))]
    public class Building
    {
        private static List<Building> _extent = new List<Building>();

        private string _name;
        private bool _isAccessible;
        
        private Location _location; 
        
        private Shop? _shop;
        private Gym? _gym;
        private Pokecenter? _pokecenter;

        public Shop? Shop { get => _shop; set => _shop = value; }
        public Gym? Gym { get => _gym; set => _gym = value; }
        public Pokecenter? Pokecenter { get => _pokecenter; set => _pokecenter = value; }

        [XmlIgnore]
        public Location Location 
        { 
            get => _location;
            set 
            {
                _location = value;
                if (_location != null)
                {
                    _location.AddBuilding(this);
                }
            }
        }
        
        public Building() { }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Building name cannot be empty or null.");
                }
                _name = value;
            }
        }

        public bool IsAccessible
        {
            get => _isAccessible;
            set => _isAccessible = value;
        }

        public Building Set(BuildingBuilder builder)
        {
            Name = builder.Name;
            IsAccessible = builder.IsAccessible;
            
            if (builder.Location == null) throw new ArgumentNullException(nameof(builder.Location));
            _location = builder.Location;
            _location.AddBuilding(this);

            //{Disjoint, Complete}
            int typeCount = 0;
            if (builder.Shop != null) typeCount++;
            if (builder.Gym != null) typeCount++;
            if (builder.Pokecenter != null) typeCount++;

            if (typeCount != 1) throw new ArgumentException("Building must have exactly one specific type aspect.");

            _shop = builder.Shop;
            _gym = builder.Gym;
            _pokecenter = builder.Pokecenter;

            AddBuilding(this);
            return this;
        }

        private static void AddBuilding(Building building) => _extent.Add(building);
        public static List<Building> GetExtent() => new List<Building>(_extent);
        
        public static void Save(string path = "buildings.xml")
        {
            Serializer.Save(path, _extent);
        }
        
        public void DeleteBuilding()
        {
            if (Shop != null) Shop.RemoveFromExtent(Shop);
            if (Gym != null) Gym.RemoveFromExtent(Gym);
            if (Pokecenter != null) Pokecenter.RemoveFromExtent(Pokecenter);
            
            _extent.Remove(this);
        }
        
        public static bool Load(string path = "buildings.xml")
        {
            var loadedList = Serializer.Load(path, _extent);
        
            if (loadedList != null)
            {
                _extent = loadedList;
                return true;
            }
            return false;
        }

        public static void RemoveFromExtent(Building b) => _extent.Remove(b);
    }

    public class BuildingBuilder
    {
        private Building _building;
        public string Name { get; }
        public bool IsAccessible { get; }
        public Location Location { get; }

        public Shop? Shop { get; private set; }
        public Gym? Gym { get; private set; }
        public Pokecenter? Pokecenter { get; private set; }

        public BuildingBuilder(string name, bool isAccessible, Location location)
        {
            Name = name;
            IsAccessible = isAccessible;
            Location = location;
            _building = new Building();
        }

        public BuildingBuilder AsShop(double multiplier)
        {
            Shop = new Shop(_building, multiplier);
            return this;
        }

        public BuildingBuilder AsGym(string leader, string badgeName)
        {
            Gym = new Gym(_building, leader, badgeName);
            return this;
        }

        public BuildingBuilder AsPokecenter(int pcNumber, string nurseName, int age)
        {
            Pokecenter = new Pokecenter(_building, pcNumber, nurseName, age);
            return this;
        }

        public Building Build() => _building.Set(this);
    }
}