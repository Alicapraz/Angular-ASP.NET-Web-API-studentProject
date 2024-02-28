import { Component, OnInit, ViewChild } from '@angular/core';
import { StudentService } from './student.service';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Student } from '../models/ui-models/student.model';
import { MatTableDataSource } from '@angular/material/table';
import {MatTableModule} from '@angular/material/table';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import { FormsModule } from '@angular/forms';
import { ViewStudentComponent } from './view-student/view-student.component';
import {MatIconModule} from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';


@Component({
  selector: 'app-students',
  standalone: true,
  imports: [HttpClientModule, MatTableModule,MatPaginator,MatPaginatorModule, FormsModule,ViewStudentComponent,MatIconModule,RouterLink,MatButtonModule],
  templateUrl: './students.component.html',
  styleUrl: './students.component.css'
})
export class StudentsComponent implements OnInit{

  students: Student[]= [];
  displayedColumns: string[] = ['firstName', 'lastName', 'dateOfBirth', 'email', 'mobile', 'gender', 'edit'];
  dataSource:MatTableDataSource<Student> = new MatTableDataSource<Student>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  filterString = '';


  constructor(private studentService:StudentService,private httpClient:HttpClient) { }
  
  ngOnInit(): void {
    
    this.studentService.getStudents().subscribe({
      next: (success) => {
        this.students = success;
        this.dataSource = new MatTableDataSource<Student>(this.students);
        this.dataSource.paginator=this.paginator;
    },

    error: (e) => {
        console.error(e);
    }

    });
  }

  filterStudents()
  {
    
    this.dataSource.filter = this.filterString.trim().toLocaleLowerCase();
  }

}
