using Models;

Nature brave = new Nature("Brave", 1, 2);
Pokemon charmander = new PokemonBuilder(1, "Charmander", 20, 1, 40, new int[]{1,1,1,1,1,1}, brave)
                                        .FireType(36)
                                        .LandEggType(10)
                                        .Build();
Pokemon vulpix = new PokemonBuilder(1, "Vulpix", 20, 1, 40, new int[]{1,1,1,1,1,1}, brave)
                                .FireType(37)
                                .LandEggType(10)
                                .Build();


Pokemon.Save("pokemons.xml");

Pokemon.Load("pokemons.xml");

List<Pokemon> allPokemons = Pokemon.GetPokemons();