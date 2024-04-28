import { PatientService } from './../../services/patient.service';
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TakeMeasurementComponent } from "../take-measurement/take-measurement.component";
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { HttpClientModule } from '@angular/common/http';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { Patient } from '../../models/patient.model';
import { MatDialog, MatDialogTitle, MatDialogContent } from '@angular/material/dialog';
import { NewPatientDialogComponent } from '../new-patient-dialog/new-patient-dialog.component';

@Component({
  selector: 'app-list-patients',
  standalone: true,
  imports: [
    TakeMeasurementComponent,
    CommonModule,
    MatButtonToggleModule,
    HttpClientModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatIconModule,
    MatDialogTitle,
    MatDialogContent,
  ],
  templateUrl: './list-patients.component.html',
  styleUrl: './list-patients.component.scss',
})
export class ListPatientsComponent implements OnInit{

  patientSSNSearch: FormControl = new FormControl('', [Validators.required, Validators.pattern('[0-9]{10}')]);
  patients: Observable<Patient[]> = new Observable<Patient[]>();

  patientSSN: FormControl = new FormControl('', [Validators.required, Validators.pattern('[0-9]{10}')]);
  patientName: FormControl = new FormControl('', [Validators.required]);
  patientEmail: FormControl = new FormControl('', [Validators.required, Validators.email]);

  itemChosen: boolean = false;

  constructor(private patientService: PatientService, public dialog: MatDialog) {}

  ngOnInit(): void {
      this.patients = this.patientService.getPatients();
  }

  setItemChosen(): void {
    this.itemChosen = true;
  }

  DeletePatient(): void {
    this.patientService.deletePatient();
  }

  NewPatient(): void {

    this.dialog.open(NewPatientDialogComponent, {
      data: {},
      
    });
  }

  FindPatient(): void {
    this.patients = this.patientService.getPatient(this.patientSSNSearch.value);
  }
}
