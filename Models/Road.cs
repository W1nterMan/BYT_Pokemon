
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
        
        public static void Save(string path = "roads.xml")
        {
            Serializer.Save(path, _extent);
        }
        
        public static bool Load(string path = "roads.xml")
        {
            var loadedList = Serializer.Load(path, _extent);
        
            if (loadedList != null)
            {
                _extent = loadedList;
                return true;
            }
            return false;
        }
        
        private HashSet<Location> _locations = new HashSet<Location>();

        public HashSet<Location> GetRoadLocations() => new HashSet<Location>(_locations);

        public void AddLocation(Location location)
        {
            if (_locations.Contains(location))
            {
                return;
            }
            
            bool added = false;
            
            try
            {
                _locations.Add(location);
                added = true;
                location.AddRoad(this);
            }
            catch (Exception e)
            {
                if (added)
                {
                    _locations.Remove(location);
                }
            }
        }

        public void RemoveLocation(Location location)
        {
            if (!_locations.Contains(location))
            {
                return;
            }

            if (_locations.Count <= 1)
            {
                throw new Exception("Road must be connected to at least one location");
            }
            
            bool removed = false;
            
            try
            {
                _locations.Remove(location);
                removed = true;
                location.RemoveRoad(this);
            }
            catch (Exception e)
            {
                if (removed)
                {
                    _locations.Add(location);
                }
            }
        }
    }
}

public enum TerrainType
{
    Field,
    Cave,
    Mountains
}