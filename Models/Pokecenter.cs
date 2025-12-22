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

        private Nurse _nurse;

        public Nurse Nurse
        {
            get => _nurse;
            set
            {
                if (value == null) throw new ArgumentException("Nurse cannot be null.");
                _nurse = value;
            }
            
        }

        public void AddPc(int computerNumber)
        {
            if (_pc != null) throw new InvalidOperationException("This Pokecenter already has a PC");

            _pc = new PC(computerNumber, this);
        }

        public void AddNurse(string name, int age)
        {
            if (_nurse != null) throw new InvalidOperationException("This Pokecenter already has a nurse");

            //we double assign _nurse, here, and in constructor respectively, maybe we want to do just new Nurse(_,_,this)
            //so object itself will assign itself to pokecenter?
            _nurse = new Nurse(name, age, this);
        }

        public void DeletePokecenter()
        {
            if (_pc != null)
            {
                PC.RemoveFromExtent(_pc);
                _pc = null;
            }

            if (_nurse == null)
            {
                Person.RemoveFromExtent(_nurse);
                _nurse = null;
            }

            RemoveFromExtent(this);
        }

        public Pokecenter() { }

        public Pokecenter(string name, bool isAccessible, Location location, int pcNumber, string nurseName, int age) : base(name, isAccessible, location)
        {
            AddPc(pcNumber);
            //we either add composition for 1-1 or make connection 0..1-1
            AddNurse(nurseName,age);
        }
    }
}