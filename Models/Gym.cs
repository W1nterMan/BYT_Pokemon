

namespace Models
{
    [Serializable]
    public class Gym : Building
    {
        private string _leader;
        
        public string? BadgeName { get; set; }
        
        public int MinRequiredBadges { get; set; }
        public List<string> TrainersInGym { get; set; } = new List<string>();

        public string Leader
        {
            get => _leader;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("Leader name required.");
                _leader = value;
            }
        }
        
        public int TrainersCount
        {
            get
            {
                return TrainersInGym.Count;
            }
        }

        public Gym() { }

        public Gym(string name, bool isAccessible, string leader) : base(name, isAccessible)
        {
            Leader = leader;
        }
    }
}