

namespace Models
{
    [Serializable] 
    public class Coordinates
    {
        private int _x;
        private int _y;

        public int X
        {
            get => _x;
            set
            {
                if (value < 0) throw new ArgumentException("X coordinate cannot be negative."); 
                _x = value;
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                if (value < 0) throw new ArgumentException("Y coordinate cannot be negative.");
                _y = value;
            }
        }

        public Coordinates() { }

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public override string ToString() => $"({X}, {Y})";
    }
}