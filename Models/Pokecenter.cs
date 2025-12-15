

namespace Models
{
    [Serializable]
    public class Pokecenter : Building
    {
        //Attributes
        private static double _baseHealingCost = 0; 
        
        public static double BaseHealingCost
        {
            get => _baseHealingCost;
            set
            {
                if (value < 0) throw new ArgumentException("Healing cost cannot be negative.");
                _baseHealingCost = value;
            }
        }
        
        //Associations
        private PC _pc;

        public PC Pc 
        { 
            get => _pc; 
            set => _pc = value; 
        }
        
        public void AddPc(int computerNumber)
        {
            if (_pc != null) throw new InvalidOperationException("This Pokecenter already has a PC");
            
            _pc = new PC(computerNumber, this);
        }
        
        public void DeletePokecenter()
        {
            if (_pc != null)
            {
                PC.RemoveFromExtent(_pc);
                _pc = null;
            }
            RemoveFromExtent(this);
        }
        
        public Pokecenter() { }

        public Pokecenter(string name, bool isAccessible, Location location, int pcNumber) : base(name, isAccessible, location)
        {
            AddPc(pcNumber);
        }
    }
}