import { HttpClientModule } from '@angular/common/http';
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
import { ListMeasurementsComponent } from '../../../shared/components/list-measurements/list-measurements.component';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

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
    HttpClientModule,
  ],
})
export class PatientPageComponent {
  loggedIn: boolean = false;
  ssndate = new FormControl('', [Validators.required, Validators.pattern('[0-9]{6}')]);
  ssnsec = new FormControl('', [Validators.required, Validators.pattern('[0-9]{4}')]);
  fullSSN: string = '';

  LogIn() {

    this.fullSSN = this.ssndate.value! + this.ssnsec.value!;

    if (this.ssndate.valid && this.ssnsec.valid && this.fullSSN.length == 10 && !isNaN(Number(this.fullSSN))) {
      this.loggedIn = true;

    }else{
      alert('Please enter a valid SSN');
      
    }
  }
}
