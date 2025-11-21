
namespace Models
{
    [Serializable]
    public class Bush
    {
        private double _encounterChance;

        public bool IsActive { get; set; }

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
        }

        public void InvestigateBush() 
        { 
        }
    }
}