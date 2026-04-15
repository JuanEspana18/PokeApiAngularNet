import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PokemonFacadeService } from '../services/pokemon-facade.service';
import { PokemonCardComponent } from '../components/pokemon-card/pokemon-card.component';
import { PokemonStatsComponent } from '../components/pokemon-stats/pokemon-stats.component';
import { PokemonEvolutionComponent } from '../components/pokemon-evolution/pokemon-evolution.component';

@Component({
  selector: 'app-pokemon-home',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    PokemonCardComponent,
    PokemonStatsComponent,
    PokemonEvolutionComponent
  ],
  templateUrl: './pokemon-home.component.html'
})
export class PokemonHomeComponent {
  readonly facade = inject(PokemonFacadeService);
  searchTerm = 'pikachu';

  constructor() {
    this.facade.loadPokemon(this.searchTerm);
  }

  search(): void {
    this.facade.loadPokemon(this.searchTerm);
  }
}
