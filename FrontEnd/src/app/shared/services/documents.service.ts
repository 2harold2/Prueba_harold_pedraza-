import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Documents } from '../models/document.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DocumentsService {

  private readonly url = "https://localhost:7035/api";
  private http: HttpClient = inject(HttpClient);

  public getAllDocuments(): Observable<Documents[]> {
    return this.http.get<Documents[]>(`${this.url}/GetAllDocuments`);
  }
}
