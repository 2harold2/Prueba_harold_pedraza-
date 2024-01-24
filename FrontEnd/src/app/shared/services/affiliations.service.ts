import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Affiliations } from '../models/affiliation.model';

@Injectable({
  providedIn: 'root'
})
export class AffiliationsService {

  private readonly url = "https://localhost:7035/api";
  private http: HttpClient = inject(HttpClient);

  public getAllAffiliations(): Observable<Affiliations[]> {
    return this.http.get<Affiliations[]>(`${this.url}/GetAllAffilations`);
  }
}
