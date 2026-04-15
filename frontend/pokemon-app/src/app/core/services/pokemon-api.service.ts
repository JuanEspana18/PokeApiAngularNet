import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PokemonBasic, PokemonDetail, PokemonEvolution } from '../../features/pokemon/models/pokemon.model';

@Injectable({ providedIn: 'root' })
export class PokemonApiService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/pokemon`;

  getPokemon(nameOrId: string): Observable<PokemonDetail> {
    return this.http.get<PokemonDetail>(`${this.baseUrl}/${nameOrId}`);
  }

  getBasicPokemon(nameOrId: string): Observable<PokemonBasic> {
    return this.http.get<PokemonBasic>(`${this.baseUrl}/${nameOrId}/basic`);
  }

  getEvolution(nameOrId: string): Observable<PokemonEvolution[]> {
    return this.http.get<PokemonEvolution[]>(`${this.baseUrl}/${nameOrId}/evolution`);
  }

  search(name: string): Observable<PokemonBasic[]> {
    return this.http.get<PokemonBasic[]>(`${this.baseUrl}/search`, {
      params: { name }
    });
  }
}
