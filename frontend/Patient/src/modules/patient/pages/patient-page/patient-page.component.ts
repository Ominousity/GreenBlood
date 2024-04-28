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
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { PatientService } from '../../services/patient.service';
import { RouterLink } from '@angular/router';
import { TakeMeasurementComponent } from '../../components/take-measurement/take-measurement.component';

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
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterLink
  ],
})
export class PatientPageComponent {
  loggedIn: boolean = false;
  ssndate = new FormControl('', [Validators.required, Validators.pattern('[0-9]{6}')]);
  ssnsec = new FormControl('', [Validators.required, Validators.pattern('[0-9]{4}')]);
  fullSSN: string = '';

  lat: number = 0;
  lng: number = 0;

  approved: boolean = false;

  constructor(public patientService: PatientService) { }

  ngOnInit(): void {
    this.getLocation();
    
  }

  getLocation() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition((position) => {
        if (position) {
          this.lat = position.coords.latitude;
          this.lng = position.coords.longitude;
          console.log(this.lat);
          console.log(this.lng);
        }

        //if position is within denmark set approved to true
        if (this.lat > 54.5 && this.lat < 57.5 && this.lng > 8 && this.lng < 15) {
          this.approved = true;
        } else {
          alert("You are not in Denmark");
        }

      },
        (error) => 
          alert("You refused to let me know where you are! >:(")
      );
    } else {
      alert("Geolocation is not supported by this browser.");
    }
  }

  LogIn() {

    this.fullSSN = this.ssndate.value! + this.ssnsec.value!;

    this.patientService.getPatient(this.fullSSN).subscribe(
      (data) => {
        if (data) {
          this.loggedIn = true;
        } else {
          alert('Patient not found');
        }
      },
      (error) => {
        alert('Patient not found');
      }
    );
  }
}
