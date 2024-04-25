import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environment/environment';
import { Observable } from 'rxjs';
import { Patient } from '../models/patient.model';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  private readonly patientAPI = environment.patientAPI

  constructor(private http: HttpClient) { 
  }

  getPatients(): Observable<Patient[]> {
    return this.http.get<Patient[]>('https://jsonplaceholder.typicode.com/posts');
  }

  getPatient(SSN: string): Observable<Patient[]> {
    return this.http.get<Patient[]>('https://jsonplaceholder.typicode.com/posts/1');
  }

  addPatient(patient: Patient) {
    return this.http.post('https://jsonplaceholder.typicode.com/posts', patient);
  }

  deletePatient() {
    return this.http.delete('https://jsonplaceholder.typicode.com/posts/1');
  }
}
