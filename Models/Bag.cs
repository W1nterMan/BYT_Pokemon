namespace Models;

public class Bag
{
    // public Bag(Trainer owner)
    // {
    //     _owner = owner ?? throw new ArgumentNullException(nameof(owner));
    // }
    //
    // public void StorePokemon(object pokemon)
    // {
    //     if (pokemon == null) throw new ArgumentNullException(nameof(pokemon));
    //     if (Pokemons.Count >= 6) throw new InvalidOperationException("Invalid bag configuration.");
    //     Pokemons.Add(pokemon);
    // }
    //
    // public object TakePokemon(object pokemon)
    // {
    //     if (pokemon == null) throw new ArgumentNullException(nameof(pokemon));
    //     if (!Pokemons.Contains(pokemon)) throw new InvalidOperationException("Pokemon not in bag.");
    //     Pokemons.Remove(pokemon);
    //     return pokemon;
    // }
    //
    // public IEnumerable<object> OpenBag() => Pokemons;
}