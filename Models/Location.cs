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
            StreamWriter file = File.CreateText(path);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Location>));
            using (XmlTextWriter writer = new XmlTextWriter(file))
            {
                xmlSerializer.Serialize(writer, _extent);
            }
        }

        public static bool load(string path = "locations.xml")
        {
            StreamReader file;
            try { file = File.OpenText(path); }
            catch (FileNotFoundException) { _extent.Clear(); return false; }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Location>));
            using (XmlTextReader reader = new XmlTextReader(file))
            {
                try { _extent = (List<Location>)xmlSerializer.Deserialize(reader); }
                catch (Exception) { _extent.Clear(); return false; }
            }
            return true;
        }
    }
}