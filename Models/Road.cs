
using System.Xml;
using System.Xml.Serialization;

namespace Models
{
    [Serializable]
    public class Road
    {
        private static List<Road> _extent = new List<Road>();

        //Attributes
        private int _number;
        private TerrainType _terrainType;
        
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
        
        //Association
        
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
        
        private HashSet<Bush> _bushes = new HashSet<Bush>();
        
        public HashSet<Bush> GetBushes() => new HashSet<Bush>(_bushes);
        
        public void AddBush(Bush bush)
        {
            if (bush == null) throw new ArgumentNullException(nameof(bush));
            if (_bushes.Contains(bush)) return;
            if (bush.Road != this) throw new Exception("Bush belongs to different Road");
            bush.Road = this;
            _bushes.Add(bush);
        }

        public void RemoveBush(Bush bush)
        {
            if (!_bushes.Contains(bush)) return;
            
            if (_bushes.Count <= 1)
            {
                throw new InvalidOperationException("Cannot remove the last Bush. A Road must have at least one Bush.");
            }

            _bushes.Remove(bush);
            Bush.RemoveFromExtent(bush);
        }
        
        public void DeleteRoad()
        {
            foreach (var road in new List<Road>(_connectedRoads))
            {
                DisconnectFromRoad(road);
            }
            
            var bushesToDelete = new List<Bush>(_bushes);
            foreach(var b in bushesToDelete)
            {
                Bush.RemoveFromExtent(b);
            }
            _bushes.Clear();
            
            foreach(var loc in new List<Location>(_locations))
            {
                RemoveLocation(loc);
            }
            
            _extent.Remove(this);
        }
        
        private HashSet<Road> _connectedRoads = new HashSet<Road>();
        public HashSet<Road> GetConnectedRoads() => new HashSet<Road>(_connectedRoads);
        
        public void ConnectToRoad(Road otherRoad)
        {
            if (otherRoad == null) throw new ArgumentNullException(nameof(otherRoad));
            if (otherRoad == this) throw new ArgumentException("A road cannot connect to itself");
            
            if (_connectedRoads.Contains(otherRoad)) return;

            _connectedRoads.Add(otherRoad);
            
            otherRoad.ConnectToRoad(this);
        }
        
        public void DisconnectFromRoad(Road otherRoad)
        {
            if (!_connectedRoads.Contains(otherRoad)) return;

            _connectedRoads.Remove(otherRoad);
            
            otherRoad.DisconnectFromRoad(this);
        }
        
        private static void AddRoad(Road road)
        {
            if (road == null) throw new ArgumentException("Road cannot be null");
            _extent.Add(road);
        }
        
        public static List<Road> GetExtent() => new List<Road>(_extent);
        
        public Road() { }

        public Road(int number, TerrainType terrainType, Location location)
        {
            Number = number;
            TerrainType = terrainType;
            location.AddRoad(this);
            _locations.Add(location);
            
            AddRoad(this);
        }
        
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
    }
}

public enum TerrainType
{
    Field,
    Cave,
    Mountains
}