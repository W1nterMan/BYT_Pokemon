using Models;

Fire charmander = new Fire(1, "Charmander", 20, 1, 40, new int[]{1,1,1,1,1,1}, 36);
Fire vulpix = new Fire(2, "Vulpix", 18, 1, 35, new int[]{1,2,1,1,1,1}, 37);

Pokemon.Save("pokemons.xml");

Pokemon.Load("pokemons.xml");

List<Pokemon> allPokemons = Pokemon.GetPokemons();