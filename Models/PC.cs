
namespace Models
{
    [Serializable]
    public class PC
    {
        //extent? -> When we link classes together pc will be serialized and stored with pokecenter so no need to store it by itself
        private int _computerNumber;

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

        public PC(int computerNumber)
        {
            ComputerNumber = computerNumber;
            //extent.add? -> line 7
        }

        public void AccessStorage()
        {
            
        }
    }
}