import { Component } from '@angular/core';
import { FormControl, FormsModule, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose, MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { PatientService } from '../../services/patient.service';
import { Patient } from '../../models/patient.model';

@Component({
  selector: 'app-new-patient-dialog',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
    ReactiveFormsModule
  ],
  templateUrl: './new-patient-dialog.component.html',
  styleUrl: './new-patient-dialog.component.scss'
})
export class NewPatientDialogComponent {

  SSN: FormControl = new FormControl('', [Validators.required, Validators.pattern('[0-9]{10}')]);
  Name: FormControl = new FormControl('', [Validators.required]);
  Email: FormControl = new FormControl('', [Validators.required, Validators.email]);

  constructor(public patientService: PatientService, public dialog: MatDialog) {}
  
  NewPatient(): void {

    let patient: Patient = {
      SSN: this.SSN.value,
      Mail: this.Name.value,
      Name: this.Email.value,
      Measurements: []
    };

    this.patientService.addPatient(patient);

    this.Cancel();
  }

  Cancel(): void {
    this.dialog.closeAll();
  }
}
