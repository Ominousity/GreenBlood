import { Component } from '@angular/core';
import { FormControl, FormsModule, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

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

  constructor(public dialogRef: MatDialogRef<NewPatientDialogComponent>) {}
  
  Submit(): void {
    this.dialogRef.close({
      SSN: this.SSN.value,
      Name: this.Name.value,
      Email: this.Email.value
    });
  }

  Cancel(): void {
    this.dialogRef.close();
  }
}
