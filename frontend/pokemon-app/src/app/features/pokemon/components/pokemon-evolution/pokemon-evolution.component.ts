import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PokemonEvolution } from '../../models/pokemon.model';

@Component({
  selector: 'app-pokemon-evolution',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './pokemon-evolution.component.html'
})
export class PokemonEvolutionComponent {
  @Input({ required: true }) evolutionChain: PokemonEvolution[] = [];
}
