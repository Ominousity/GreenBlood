import { Routes } from '@angular/router';
import { DoctorPageComponent } from '../modules/docter/page/doctor-page/doctor-page.component';
import { PatientPageComponent } from '../modules/patient/pages/patient-page/patient-page.component';
import { HomePageComponent } from '../modules/home/pages/home-page/home-page.component';

export const routes: Routes = [
    
    { path: 'home', component: HomePageComponent },
    { path: 'doctor', component: DoctorPageComponent },
    { path: 'patient', component: PatientPageComponent },
    { path: '**', redirectTo: '/home' },
];
