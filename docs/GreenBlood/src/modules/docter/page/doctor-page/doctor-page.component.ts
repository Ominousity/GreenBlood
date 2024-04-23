import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { TakeMeasurementComponent as TakeMeasurementComponent } from "../../../shared/components/take-measurement/take-measurement.component";
import { ListMeasurementsComponent } from "../../../shared/list-measurements/list-measurements.component";

@Component({
    selector: 'app-doctor-page',
    standalone: true,
    templateUrl: './doctor-page.component.html',
    styleUrl: './doctor-page.component.scss',
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
        ListMeasurementsComponent
    ]
})
export class DoctorPageComponent {
  
}
