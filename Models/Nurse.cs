using Models;

namespace Models;

[Serializable]
public class Nurse : Person
{
    public static string NurseNickname { get; } = "Nurse Joy";

    public Nurse() { }

    public Nurse(string name, int age) : base(name,age)
    {
        
    }
}