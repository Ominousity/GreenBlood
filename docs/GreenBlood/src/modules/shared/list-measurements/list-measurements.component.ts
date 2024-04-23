import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatListModule } from '@angular/material/list';
import { TakeMeasurementComponent } from "../components/take-measurement/take-measurement.component";
import { Measurement } from '../models/measurement.model';

@Component({
    selector: 'app-list-measurements',
    standalone: true,
    templateUrl: './list-measurements.component.html',
    styleUrl: './list-measurements.component.scss',
    imports: [
        MatExpansionModule,
        MatListModule,
        TakeMeasurementComponent,
        CommonModule
    ]
})
export class ListMeasurementsComponent {
    measurements: Measurement[] = [];
    itemChosen: boolean = false;

}
