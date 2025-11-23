
using System.Xml;
using System.Xml.Serialization;

namespace Models
{
    [Serializable]
    public class Road
    {
        private static List<Road> _extent = new List<Road>();

        private int _number;
        private string _terrainType; //TODO:maybe add some defined list of possible types
        
        public Road() { }

        public Road(int number, string terrainType)
        {
            Number = number;
            TerrainType = terrainType;
            
            addRoad(this);
        }

        public int Number
        {
            get => _number;
            set
            {
                if (value <= 0) throw new ArgumentException("Road number must be positive.");
                _number = value;
            }
        }

        public string TerrainType
        {
            get => _terrainType;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("Terrain type cannot be empty.");
                _terrainType = value;
            }
        }
        
        private static void addRoad(Road road)
        {
            if (road == null) throw new ArgumentException("Road cannot be null");
            _extent.Add(road);
        }
        
        public static List<Road> GetExtent() => new List<Road>(_extent);
        
        public static void save(string path = "roads.xml")
        {
            StreamWriter file = File.CreateText(path);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Road>));
            using (XmlTextWriter writer = new XmlTextWriter(file))
            {
                xmlSerializer.Serialize(writer, _extent);
            }
        }
        
        public static bool load(string path = "roads.xml")
        {
            StreamReader file;
            try
            {
                file = File.OpenText(path);
            }
            catch (FileNotFoundException)
            {
                _extent.Clear();
                return false;
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Road>));
            using (XmlTextReader reader = new XmlTextReader(file))
            {
                try
                {
                    _extent = (List<Road>)xmlSerializer.Deserialize(reader);
                }
                catch (Exception)
                {
                    _extent.Clear();
                    return false;
                }
            }
            return true;
        }
    }
}