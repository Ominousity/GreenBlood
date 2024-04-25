import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Measurement } from '../../../shared/models/measurement.model';
import { MeasurementService } from '../../services/measurement.service';

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
  @Input() isViewing: boolean | undefined;

  systolic: FormControl = new FormControl(0, [Validators.required, Validators.pattern('[0-9]{4}')]);
  diastolic: FormControl = new FormControl(0, [Validators.required, Validators.pattern('[0-9]{4}')]);
  patientSSN: FormControl = new FormControl(0, [Validators.required, Validators.pattern('[0-9]{10}')]);


  constructor(public measurementService: MeasurementService) {}

  SendMeasurement() {
    let measurement: Measurement = {
      Id: 0,
      Date: new Date(),
      Systolic: this.systolic.value,
      Diastolic: this.diastolic.value,
      Seen: false,
    };

    this.measurementService.addMeasurement(measurement)
  }
}
