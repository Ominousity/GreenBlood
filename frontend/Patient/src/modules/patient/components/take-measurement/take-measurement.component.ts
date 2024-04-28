import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import {
  FormControl,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Measurement } from '../../models/measurement.model';
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
    TakeMeasurementComponent,
  ],
  templateUrl: './take-measurement.component.html',
  styleUrl: './take-measurement.component.scss',
})
export class TakeMeasurementComponent {
  @Input() isUpdating: boolean | undefined;
  @Input() measurement: Measurement | undefined;
  @Input() SSN: string = '';

  systolic: FormControl = new FormControl(0, [
    Validators.required,
    Validators.pattern('[0-9]{4}'),
  ]);
  diastolic: FormControl = new FormControl(0, [
    Validators.required,
    Validators.pattern('[0-9]{4}'),
  ]);
  seen: FormControl = new FormControl(false, [Validators.required]);

  constructor(public measurementService: MeasurementService) {}

  SendMeasurement() {
    let measurement: Measurement = {
      id: 0,
      date: new Date(),
      systolic: this.systolic.value,
      diastolic: this.diastolic.value,
      seen: false,
    };
    if (measurement.systolic > 0 && measurement.diastolic > 0) {
      this.measurementService
        .addMeasurement(measurement, this.SSN)
        .subscribe((data) => {
          alert('Measurement added');
          this.systolic.setValue(0);
          this.diastolic.setValue(0);
        });
    } else {
      alert('Please enter a valid measurement');
    }
  }
}
