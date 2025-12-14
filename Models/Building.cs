using System.Xml;
using System.Xml.Serialization;

namespace Models
{
    [Serializable]
    [XmlInclude(typeof(Shop))]
    [XmlInclude(typeof(Gym))]
    [XmlInclude(typeof(Pokecenter))]
    public abstract class Building
    {
        private static List<Building> _extent = new List<Building>();

        private string _name;
        private bool _isAccessible;
        
        private Location _location; 
        
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

        public Building(string name, bool isAccessible, Location location)
        {
            Name = name;
            IsAccessible = isAccessible;
            
            if (location == null) throw new ArgumentNullException(nameof(location), "Building must be placed in a Location.");
            
            _location = location;
            _location.AddBuilding(this);

            AddBuilding(this);
        }
        
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
        
        private static void AddBuilding(Building building)
        {
            if (building == null) 
            {
                throw new ArgumentException("Building cannot be null"); 
            }
            _extent.Add(building);
        }

        public static List<Building> GetExtent()
        {
            return new List<Building>(_extent);
        }
        
        public static void Save(string path = "buildings.xml")
        {
            Serializer.Save(path, _extent);
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
        
        public static void RemoveFromExtent(Building building)
        {
            if(_extent.Contains(building))
            {
                _extent.Remove(building);
            }
        }
    }
}