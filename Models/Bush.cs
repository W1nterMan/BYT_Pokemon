
namespace Models
{
    [Serializable]
    public class Bush
    {
        //where is extent? static? -> When we link classes together bush will be serialized and stored with road so no need to store it by itself
        // I assume encounter chance may differ from bush to bush maybe it will be somethin like math.random() during creation idk can be static if not
        private double _encounterChance;
        
        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => _isActive = value;
        }


        public double EncounterChance
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
        
        public Bush() { }

        public Bush(bool isActive, double encounterChance)
        {
            IsActive = isActive;
            EncounterChance = encounterChance;
            //extend.add? -> Line 7,8
        }

        public void InvestigateBush() 
        { 
        }
    }
}