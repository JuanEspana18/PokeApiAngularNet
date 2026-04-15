export interface PokemonType {
  name: string;
}

export interface PokemonAbility {
  name: string;
  isHidden: boolean;
}

export interface PokemonStat {
  name: string;
  value: number;
}

export interface PokemonEvolution {
  name: string;
}

export interface PokemonBasic {
  id: number;
  name: string;
  imageUrl: string;
  height: number;
  weight: number;
  types: PokemonType[];
  abilities: PokemonAbility[];
  stats: PokemonStat[];
}

export interface PokemonDetail extends PokemonBasic {
  baseHappiness: number;
  color: string;
  habitat: string;
  isLegendary: boolean;
  isMythical: boolean;
  evolutionChain: PokemonEvolution[];
}
