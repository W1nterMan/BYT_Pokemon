namespace Models;

public class Trainer
{
    public int Id { get; set; }
    public Badge[] Badges { get; set; }
    public int TotalMoney { get; set; }
    public string Status { get; set; }      //i dont remember why we needed this :P

    public ICollection<Battle>? Type { get; set; }
    
    public ICollection<PokemonInBag> PokemonsInBag { get; set; }    //can change to ireadonlylist<>, TODO:0..6 limit in methods check.

    public Trainer()
    {
        //constructor...
    }
}

public class Badge
{
    //complex attribute(?)
    public string Name { get; set; }
    public string Color { get; set; }
}