import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { TakeMeasurementComponent as TakeMeasurementComponent } from '../../../shared/components/take-measurement/take-measurement.component';
import { ListMeasurementsComponent } from '../../../shared/list-measurements/list-measurements.component';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-patient-page',
  standalone: true,
  templateUrl: './patient-page.component.html',
  styleUrl: './patient-page.component.scss',
  imports: [
    CommonModule,
    MatCardModule,
    MatFormFieldModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatTabsModule,
    MatToolbarModule,
    TakeMeasurementComponent,
    ListMeasurementsComponent,
    FormsModule,
    ReactiveFormsModule,
  ],
})
export class PatientPageComponent {
    loggedIn: boolean = false;
    ssndate = new FormControl(''); 
    ssnsec = new FormControl('');

    LogIn() {
      let ssn: string = this.ssndate.value! + this.ssnsec.value!;

      if (ssn.length === 10 && !isNaN(Number(ssn))) {
          this.loggedIn = true;
      }else{
          console.log(ssn.length, isNaN(Number(ssn)))
          alert('Please enter a valid SSN');
      }
    }
}
