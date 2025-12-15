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
        
        public static void Save(string path = "locations.xml")
        {
            Serializer.Save(path, _extent);
        }

        public static bool Load(string path = "locations.xml")
        {
            var loadedList = Serializer.Load(path, _extent);
        
            if (loadedList != null)
            {
                _extent = loadedList;
                return true;
            }
            return false;
        }

        private HashSet<Road> _roads = new HashSet<Road>();
        
        public HashSet<Road> GetLocationRoads() => new HashSet<Road>(_roads);

        public void AddRoad(Road road)
        {
            if (_roads.Contains(road))
            {
                return;
            }
            
            bool added = false;
            
            try
            {
                _roads.Add(road);
                added = true;
                road.AddLocation(this);
            }
            catch (Exception e)
            {
                if (added)
                {
                    _roads.Remove(road);
                }
            }
            
        }

        public void RemoveRoad(Road road)
        {
            if (!_roads.Contains(road))
            {
                return;
            }
            
            bool removed = false;
            
            try
            {
                _roads.Remove(road);
                removed = true;
                road.RemoveLocation(this);
            }
            catch (Exception e)
            {
                if (removed)
                {
                    _roads.Add(road);
                }
            }
        }
        
        private HashSet<Building> _buildings = new HashSet<Building>();
        public HashSet<Building> GetBuildings() => new HashSet<Building>(_buildings);
        
        public void AddBuilding(Building building)
        {
            if (building == null) throw new ArgumentNullException(nameof(building));
            
            if (_buildings.Contains(building)) return;
            
            _buildings.Add(building);
            
            if (building.Location != this)
            {
                throw new Exception("Building belongs to a different location.");
            }
        }
        
        public void RemoveBuilding(Building building)
        {
            if (!_buildings.Contains(building)) return;

            _buildings.Remove(building);
            
            Building.RemoveFromExtent(building);
        }
        
        public void DeleteLocation()
        {
            var buildingsToDelete = new List<Building>(_buildings);
            
            foreach (var b in buildingsToDelete)
            {
                Building.RemoveFromExtent(b);
            }
            _buildings.Clear();
            
            _extent.Remove(this);
        }
        
    }
}

public enum LocationType
{
    City,
    Village,
    Town
}