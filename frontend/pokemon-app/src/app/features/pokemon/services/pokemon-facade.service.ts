import { Injectable, inject, signal } from '@angular/core';
import { finalize } from 'rxjs';
import { PokemonApiService } from '../../../core/services/pokemon-api.service';
import { PokemonDetail } from '../models/pokemon.model';

@Injectable({ providedIn: 'root' })
export class PokemonFacadeService {
  private readonly api = inject(PokemonApiService);

  readonly pokemon = signal<PokemonDetail | null>(null);
  readonly loading = signal(false);
  readonly error = signal<string | null>(null);

  loadPokemon(nameOrId: string): void {
    const value = nameOrId.trim();

    if (!value) {
      this.error.set('Ingresa un nombre o id.');
      this.pokemon.set(null);
      return;
    }

    this.loading.set(true);
    this.error.set(null);

    this.api.getPokemon(value)
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (response) => {
          this.pokemon.set(response);
        },
        error: (error) => {
          const message = error?.error?.message ?? 'No fue posible cargar el Pokémon.';
          this.error.set(message);
          this.pokemon.set(null);
        }
      });
  }
}
