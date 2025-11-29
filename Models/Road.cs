
using System.Xml;
using System.Xml.Serialization;

namespace Models
{
    [Serializable]
    public class Road
    {
        private static List<Road> _extent = new List<Road>();

        private int _number;
        private TerrainType _terrainType;
        
        public Road() { }

        public Road(int number, TerrainType terrainType)
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

        public TerrainType TerrainType
        {
            get => _terrainType;
            set
            {
                if (!Enum.IsDefined(typeof(TerrainType), value)) throw new ArgumentException("Invalid terrain type.");
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
            Serializer.Save(path, _extent);
        }
        
        public static bool load(string path = "roads.xml")
        {
            return Serializer.Load(path,  _extent);
        }
    }
}

public enum TerrainType
{
    Field,
    Cave,
    Mountains
}