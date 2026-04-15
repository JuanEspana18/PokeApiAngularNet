import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PokemonDetail } from '../../models/pokemon.model';

@Component({
  selector: 'app-pokemon-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './pokemon-card.component.html'
})
export class PokemonCardComponent {
  @Input({ required: true }) pokemon!: PokemonDetail;
}
