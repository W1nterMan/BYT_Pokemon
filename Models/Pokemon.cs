namespace Models;

public class Pokemon
{
    public int Id { get; set; }
    public int HealthPoints { get; set; }
    public string Name { get; set; }
    public int ExpPoints { get; set; }
    public double Weight { get; set; }
    public int[] BaseStats { get; set; } = new int[6];      // <-- [hp,attack,defence,sp.atk,sp.def,speed]
    public EvolutionMethod[] EvolutionMethods { get; set; } //actually i dont know if this shoudl exist, too complex
    public int HatchSteps { get; set; }
    public string? Status { get; set; }                     //can be "","Active","Defeated"

    public Pokemon? EvolvesTo { get; set; }

    public PokemonInBag? PokemonInBag { get; set; } 
    
    public Pokemon()
    {
        //constructor...
    }
    
}

public class EvolutionMethod
{
    //placeholder class, shouldn`t be here
    //TODO: decide to stay or delete
}