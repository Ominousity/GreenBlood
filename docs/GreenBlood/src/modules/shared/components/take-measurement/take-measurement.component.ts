import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { environment } from '../../../../environment/environment';
import { Measurement } from '../../../shared/models/measurement.model';

@Component({
  selector: 'app-take-measurement',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatFormFieldModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatTabsModule,
    MatToolbarModule,
    TakeMeasurementComponent
],
  templateUrl: './take-measurement.component.html',
  styleUrl: './take-measurement.component.scss'
})
export class TakeMeasurementComponent {
  @Input() isPatient: boolean | undefined;
  @Input() isUpdating: boolean | undefined;

  systolic: FormControl = new FormControl(0);
  diastolic: FormControl = new FormControl(0);
  patientSSN: FormControl = new FormControl('');

  API: string = environment.MeasurementAPI;


  constructor(public http: HttpClient) {}

  SendMeasurement() {
    let measurement: Measurement = {
      Date: new Date(),
      Systolic: this.systolic.value,
      Diastolic: this.diastolic.value,
      PatientSSN: this.patientSSN.value,
      Seen: false,
    };

    this.http.post(this.API, measurement)
  }
}
