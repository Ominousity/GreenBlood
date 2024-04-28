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

  addMeasurement(measurement: Measurement) {
    return this.http.post('https://jsonplaceholder.typicode.com/posts', {});
  }

  updateMeasurement() {
    return this.http.put('https://jsonplaceholder.typicode.com/posts/1', {});
  }
}
