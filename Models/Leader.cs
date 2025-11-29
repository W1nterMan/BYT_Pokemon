using Models;
namespace TestProject6.BYT_Pokemon.Models;

[Serializable]
public class Leader : Person
{
    private string _specialPrefix;

    public string SpecialPrefix
    {
        get => _specialPrefix;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Special prefix can not be empty or whitespaces.");
            _specialPrefix = value;
        }
    }

    public Leader() { }

    public Leader(string name, int age, string specialPrefix) : base(name, age)
    {
        SpecialPrefix = specialPrefix;
    }
}