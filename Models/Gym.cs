

namespace Models
{
    [Serializable]
    public class Gym : Building
    {
        private string _leader;

        private string _badgeName;
        public string BadgeName
        {
            get => _badgeName;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("Badge name required.");
                _badgeName = value;
            }
        }
        
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

        public Gym(string badgeName, bool isAccessible, string leader) : base(badgeName, isAccessible)
        {
            Leader = leader;
        }
    }
}