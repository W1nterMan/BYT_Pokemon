using System.Xml.Serialization;

namespace Models
{
    [Serializable]
    public class Pokecenter
    {
        private static List<Pokecenter> _extent = new List<Pokecenter>();
        [XmlIgnore]
        private Building _building;
        private static double _baseHealingCost = 0;

        public Building Building => _building;

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

        [XmlIgnore]
        public Nurse Nurse
        {
            get => _nurse;
            set
            {
                if (value == null) throw new ArgumentException("Nurse cannot be null.");
                _nurse = value;
            }
            
        }

        public Pokecenter() { }

        public Pokecenter(Building building, int pcNumber, string nurseName, int age)
        {
            _building = building;
            AddPc(pcNumber);
            //composition chosen.
            AddNurse(nurseName, age);
            _extent.Add(this);
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
            var person = new PersonBuilder(name, age).AsNurse(this).Build();
            _nurse = person.Nurse;
        }

        public static List<Pokecenter> GetExtent() => new List<Pokecenter>(_extent);

        public static void RemoveFromExtent(Pokecenter pokecenter)
        {
            if (pokecenter._pc != null)
            {
                PC.RemoveFromExtent(pokecenter._pc);
                pokecenter._pc = null;
            }

            if (pokecenter._nurse != null)
            {
                Nurse.RemoveFromExtent(pokecenter._nurse);
                //TODO: nurse left "orphaned" w/o pokecenter? or we delete the person behind it? we`ll need to decide.
                Person.RemoveFromExtent(pokecenter._nurse.Person);
                pokecenter._nurse = null;

            }
            
            _extent.Remove(pokecenter);   
        }
    }
}