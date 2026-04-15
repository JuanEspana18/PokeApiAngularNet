import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PokemonStat } from '../../models/pokemon.model';

@Component({
  selector: 'app-pokemon-stats',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './pokemon-stats.component.html'
})
export class PokemonStatsComponent {
  @Input({ required: true }) stats: PokemonStat[] = [];
}
