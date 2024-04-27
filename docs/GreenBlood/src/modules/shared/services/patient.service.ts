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

  getPatient(SSN: string): Observable<Patient> {
    console.log(this.patientAPI + 'get');
    return this.http.get<Patient>(this.patientAPI + 'get', { params: { SSN } });
  }

  addPatient(patient: Patient) {
    return this.http.post(this.patientAPI + 'add', patient);
  }

  deletePatient(SSN: string) {
    return this.http.delete(this.patientAPI + 'delete', { params: { SSN } });
  }
}
