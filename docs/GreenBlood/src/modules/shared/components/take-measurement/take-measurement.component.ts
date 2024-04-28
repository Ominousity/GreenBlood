import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, Input, OnInit, signal } from '@angular/core';
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
import { MatCheckboxModule } from '@angular/material/checkbox';

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
    MatCheckboxModule
],
  templateUrl: './take-measurement.component.html',
  styleUrl: './take-measurement.component.scss'
})
export class TakeMeasurementComponent implements OnInit{
  @Input() isPatient: boolean | undefined;
  @Input() isUpdating: boolean | undefined;
  @Input() isViewing: boolean | undefined;
  @Input() measurement: Measurement | undefined;
  @Input() SSN: string = '';

  systolic: FormControl = new FormControl(0, [Validators.required, Validators.pattern('[0-9]{4}')]);
  diastolic: FormControl = new FormControl(0, [Validators.required, Validators.pattern('[0-9]{4}')]);
  patientSSN: FormControl = new FormControl(0, [Validators.required, Validators.pattern('[0-9]{10}')]);
  seen: FormControl = new FormControl(false, [Validators.required]);


  constructor(public measurementService: MeasurementService) {}

  ngOnInit(): void {
    if (this.isUpdating) {
      this.patientSSN.setValue(this.SSN);
      this.systolic.setValue(this.measurement?.systolic);
      this.diastolic.setValue(this.measurement?.diastolic);
      this.seen.setValue(this.measurement?.seen);
    }
  }

  SendMeasurement() {
    if (this.isUpdating) {
      
      let measurement: Measurement = {
        id: this.measurement?.id ?? 0,
        date: this.measurement?.date ?? new Date(),
        systolic: Number(this.systolic.value),
        diastolic: Number(this.diastolic.value),
        seen: this.seen.value,
      };
  
  
      this.measurementService.updateMeasurement(measurement, this.SSN).subscribe(
        (data) => {
          alert('Measurement updated');
        }
      );
    }else {

      let measurement: Measurement = {
        id: 0,
        date: new Date(),
        systolic: this.systolic.value,
        diastolic: this.diastolic.value,
        seen: false,
      };
      if (measurement.systolic > 0 && measurement.diastolic > 0) {
        this.measurementService.addMeasurement(measurement, this.SSN).subscribe(
          (data) => {
            alert('Measurement added');
            this.systolic.setValue(0);
            this.diastolic.setValue(0);
          }
        )
      }else {
        alert('Please enter a valid measurement');
      }
    }
  }
}
