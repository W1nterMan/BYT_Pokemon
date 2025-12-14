
namespace Models
{
    [Serializable]
    public class PC
    {
        private static List<PC> _extent = new List<PC>();
        private int _computerNumber;
        
        private Pokecenter _pokecenter;
        
        public Pokecenter Pokecenter
        {
            get => _pokecenter;
            set => _pokecenter = value;
        }

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
        
        public PC() { }

        public PC(int computerNumber, Pokecenter pokecenter)
        {
            if (pokecenter == null) throw new ArgumentNullException(nameof(pokecenter), "PC must be in a Pokecenter.");
            
            ComputerNumber = computerNumber;
            _pokecenter = pokecenter;
            
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
        
        public static void RemoveFromExtent(PC pc) => _extent.Remove(pc);

        public void AccessStorage()
        {
            
        }
    }
}