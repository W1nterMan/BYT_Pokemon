using System.Xml;
using System.Xml.Serialization;

namespace Models
{
    [Serializable]
    public class Location
    {
        private static List<Location> _extent = new List<Location>();

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("Name required.");
                _name = value;
            }
        }

        private LocationType _type;
        public LocationType Type
        {
            get => _type;
            set
            {
                if (!Enum.IsDefined(typeof(LocationType), value)) throw new ArgumentException("Invalid Type.");
                _type = value;
            }
        }
        
        public Coordinates Coords { get; set; }     //complex attribute = class? -> concluded that we need to talk to teacher about this

        public Location() { }
        
        public Location(string name, int x, int y, LocationType type)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name required");
            
            Name = name;
            Coords = new Coordinates(x, y);
            Type = type;

            addLocation(this);
        }

        private static void addLocation(Location location)
        {
            if (location == null) throw new ArgumentException("Location cannot be null");
            _extent.Add(location);
        }

        public static List<Location> GetExtent() => new List<Location>(_extent);
        
        public static void save(string path = "locations.xml")
        {
            Serializer.Save(path, _extent);
        }

        public static bool load(string path = "locations.xml")
        {
            return Serializer.Load(path,  _extent);
        }
    }
}