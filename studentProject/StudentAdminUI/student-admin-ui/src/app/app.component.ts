import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { TopNavComponent } from './layout/top-nav/top-nav.component';
import { HttpClientModule } from '@angular/common/http';
import { StudentsComponent } from "./students/students.component";
import {MatTableModule} from '@angular/material/table';
import { ViewStudentComponent } from './students/view-student/view-student.component';
import { FormsModule } from '@angular/forms';


@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [CommonModule, RouterOutlet, TopNavComponent, HttpClientModule, StudentsComponent,MatTableModule, ViewStudentComponent,FormsModule]
})
export class AppComponent {
  
}
