import { Routes } from '@angular/router';
import { DoctorPageComponent } from '../modules/docter/page/doctor-page/doctor-page.component';

export const routes: Routes = [
    
    { path: 'home', component: DoctorPageComponent },
    { path: '**', redirectTo: '/home' },
];
