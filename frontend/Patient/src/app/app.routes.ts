import { Routes } from '@angular/router';
import { PatientPageComponent } from '../modules/patient/pages/patient-page/patient-page.component';

export const routes: Routes = [
    
    { path: 'home', component: PatientPageComponent },
    { path: '**', redirectTo: '/home' },
];
