using System.Xml.Serialization;

namespace Models
{
    [Serializable]
    public class Gym
    {
        private static List<Gym> _extent = new List<Gym>();
        [XmlIgnore]
        private Building _building;
        private string _leader;

        private string _badgeName;

        public Building Building => _building;

        public string BadgeName
        {
            get => _badgeName;
            set => _badgeName = value;
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

        public Gym(Building building, string leader, string badgeName)
        {
            _building = building;
            Leader = leader;
            BadgeName = badgeName;
            _extent.Add(this);
        }

        public static List<Gym> GetExtent() => new List<Gym>(_extent);
        public static void RemoveFromExtent(Gym gym) => _extent.Remove(gym);
    }
}