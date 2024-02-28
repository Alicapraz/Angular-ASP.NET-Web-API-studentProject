import { Routes } from '@angular/router';
import { StudentsComponent } from './students/students.component';
import { ViewStudentComponent } from './students/view-student/view-student.component';

export const routes: Routes = [
    {
        path:'', component:StudentsComponent
    },
    {
        path:'students', component:StudentsComponent
    },
    {
        path:'students/:id', component:ViewStudentComponent
    }
];
