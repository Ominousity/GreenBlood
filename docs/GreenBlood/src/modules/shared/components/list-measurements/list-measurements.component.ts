import { Observable, finalize } from 'rxjs';
import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TakeMeasurementComponent } from "../take-measurement/take-measurement.component";
import { Measurement } from '../../models/measurement.model';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { HttpClientModule } from '@angular/common/http';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { PatientService } from '../../services/patient.service';
import { MatButtonModule } from '@angular/material/button';

@Component({
    selector: 'app-list-measurements',
    standalone: true,
    templateUrl: './list-measurements.component.html',
    styleUrl: './list-measurements.component.scss',
    imports: [
        TakeMeasurementComponent,
        CommonModule,
        MatButtonToggleModule,
        HttpClientModule,
        MatInputModule,
        FormsModule,
        ReactiveFormsModule,
        MatButtonModule
    ]
})
export class ListMeasurementsComponent implements OnInit {
    @Input() isPatient: boolean | undefined;
    @Input() startingSSN: string = '';

    patientSSNSearch: FormControl = new FormControl('', [Validators.required, Validators.pattern('[0-9]{10}')]);

    isLoading: boolean = true;

    measurements: Measurement[] = [];
    itemChosen: boolean = false;

    constructor(private patientService: PatientService) {}

    ngOnInit(): void {
        if (this.startingSSN !== '') {
            this.patientService.getPatient(this.startingSSN).subscribe((patient) => {
                this.measurements = patient[0].Measurements;
                this.isLoading = false;
            });
        }
    }

    FindMeasurements(): void {
        this.isLoading = true;
        this.patientService.getPatient(this.patientSSNSearch.value).subscribe((patient) => {
            this.measurements = patient[0].Measurements;
            this.isLoading = false;
        });
    }
}
