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
        
        public Building() { }

        public Building(string name, bool isAccessible)
        {
            Name = name;
            IsAccessible = isAccessible;
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
            return Serializer.Load(path,  _extent);
        }
    }
}