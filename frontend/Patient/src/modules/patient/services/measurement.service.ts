import { Measurement } from './../models/measurement.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class MeasurementService {

  private readonly measurementAPI = environment.measurementAPI

  constructor(public http: HttpClient) { }

  addMeasurement(measurement: Measurement, SSN: string) {
    return this.http.post(this.measurementAPI + 'Add?SSN=' + SSN, measurement);
  }

  updateMeasurement(measurement: Measurement, SSN: string) {
    return this.http.put(this.measurementAPI + 'Update?SSN=' + SSN, measurement);
  }
}
