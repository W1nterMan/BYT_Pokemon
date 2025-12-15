
namespace Models
{
    [Serializable]
    public class Bush
    {
        private static List<Bush> _extent = new List<Bush>();
        
        //Attributes
        private static double _encounterChance = 0.1;
        private bool _isActive;
        
        public static double EncounterChance
        {
            get => _encounterChance;
            set
            {
                if (value < 0.0 || value > 1.0)
                {
                    throw new ArgumentException("Encounter chance must be between 0.0 and 1.0.");
                }
                _encounterChance = value;
            }
        }
        
        public bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }
        
        //Associations
        private Road _road;
        public Road Road 
        { 
            get => _road; 
            set 
            {
                _road = value;
            }
        }
        
        public Bush() { }

        public Bush(bool isActive, Road road)
        {
            if (road == null) throw new ArgumentNullException(nameof(road), "Bush must grow on a Road.");
            
            IsActive = isActive;
            
            Road = road;
            road.AddBush(this);
            
            _extent.Add(this);
        }
        
        public static List<Bush> GetExtent() => new List<Bush>(_extent);

        public static void Save(string path = "bushes.xml")
        {
            Serializer.Save(path, _extent);
        }
        
        public static bool Load(string path = "bushes.xml")
        {
            var loadedList = Serializer.Load(path, _extent);
        
            if (loadedList != null)
            {
                _extent = loadedList;
                return true;
            }
            return false;
        }
        
        public static void RemoveFromExtent(Bush bush) => _extent.Remove(bush);

        public void InvestigateBush() 
        { 
        }
    }
}