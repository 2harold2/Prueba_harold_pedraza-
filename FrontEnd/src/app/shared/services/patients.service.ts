import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { DetailPatient, Patient } from '../models/patient.model';

@Injectable({
  providedIn: 'root',

})
export class PatientsService {

  private readonly url = "https://localhost:7035/api";
  private http: HttpClient = inject(HttpClient);

  public getAllPatients(): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${this.url}/GetAllPatients`);
  }

  public gePatientById(id: number): Observable<DetailPatient> {
    return this.http.get<DetailPatient>(`${this.url}/GetPatientById/id?id=${id}`);
  }

  public createPatient(request: DetailPatient) {
    return this.http.post(`${this.url}/CreatePatient`, request);
  }

  public updatePatientById(request: DetailPatient) {
    return this.http.post(`${this.url}/UpdatePatient`, request);
  }

  public deletePatient(id: number) {
    return this.http.delete(`${this.url}/DeletePatient?id=${id}`);
  }
}
