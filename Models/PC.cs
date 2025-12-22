
namespace Models
{
    [Serializable]
    public class PC
    {
        private static List<PC> _extent = new List<PC>();
        
        //Attributes
        private int _computerNumber;
        
        public int ComputerNumber
        {
            get => _computerNumber;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Computer number must be greater than zero.");
                }
                _computerNumber = value;
            }
        }
        
        //Associations
        private Pokecenter _pokecenter;
        
        public Pokecenter Pokecenter
        {
            get => _pokecenter;
            set {
                if (value == null) throw new ArgumentNullException(nameof(value), "PC must be in a Pokecenter.");
                _pokecenter = value;
            }
        }
        
        public static void RemoveFromExtent(PC pc) => _extent.Remove(pc);
        
        public PC() { }

        public PC(int computerNumber, Pokecenter pokecenter)
        {
            ComputerNumber = computerNumber;
            Pokecenter = pokecenter;
            
            _extent.Add(this);
        }
        
        public static List<PC> GetExtent() => new List<PC>(_extent);

        public static void Save(string path = "pcs.xml")
        {
            Serializer.Save(path, _extent);
        }

        public static bool Load(string path = "pcs.xml")
        {
            var loadedList = Serializer.Load(path, _extent);
        
            if (loadedList != null)
            {
                _extent = loadedList;
                return true;
            }
            return false;
        }
        
        /*public void AccessStorage()
        {
            
        }*/
    }
}