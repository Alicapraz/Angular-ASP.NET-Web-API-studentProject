import { Component, OnInit } from '@angular/core';
import { StudentService } from '../student.service';
import { ActivatedRoute, RouterLink, Router } from '@angular/router';
import {MatIconModule} from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { Student } from '../../models/ui-models/student.model';
import { GenderService } from '../services/gender.service';
import { Gender } from '../../models/ui-models/gender.model';
import {MatSelectModule} from '@angular/material/select';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-view-student',
  standalone: true,
  imports: [MatIconModule,FormsModule,MatButtonModule,MatButtonToggleModule,MatFormFieldModule,MatInputModule,MatDatepickerModule,RouterLink,MatSelectModule,MatSnackBarModule,CommonModule],
  templateUrl: './view-student.component.html',
  styleUrl: './view-student.component.css'
})
export class ViewStudentComponent implements OnInit{

  studentId: string | null | undefined;
  student : Student = {
    id: '',
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    email: '',
    mobile: 0,
    genderId: '',
    profileImageUrl: '',
    gender: {
      id: '',
      description: ''
    },
    address: {
      id: '',
      physicalAddress: '',
      postalAddress: ''
    }
  }

  genderList : Gender[] = [];
  isNewStudent = false;
  header = "";
  displayProfileImageUrl = "";

  constructor(private readonly studentService:StudentService, private readonly genderService:GenderService,
     private readonly route:ActivatedRoute, private router: Router, private snackbar:MatSnackBar) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      (params) => {
        this.studentId = params.get('id');
        //studentId add ise eklemeye göre
        if(this.studentId === "add")
        {
          this.isNewStudent = true;
          this.header = "Öğrenci Ekle"
          this.setImage();
        }
        else
        {
          this.isNewStudent = false;
          this.header = "Öğrenci Düzenle"
          this.studentService.getStudent(this.studentId).subscribe({
            next: (success) => {
              this.student = success;
              this.setImage();
          },
      
          error: (e) => {
            this.setImage();
          }
      
          });
        }

        this.genderService.getGenderList().subscribe({
          next: (success) => {
            this.genderList = success;
        },
    
        error: (e) => {
            
        }
    
        });
      }
    )
  }

  onUpdate()
  {
    this.studentService.updateStudent(this.student.id, this.student).subscribe({
      next: (success) => {
        this.router.navigateByUrl('students');
        this.snackbar.open('Öğrenci güncellendi',undefined, {duration:2000});
    },

    error: (err) => {
      this.snackbar.open('Öğrenci güncellenemedi!',undefined, {duration:2000});
    }

    });
  }

  onDelete()
  {
    this.studentService.deleteStudent(this.student.id).subscribe({
      next: (success) => {
        this.snackbar.open('Öğrenci silindi!',undefined, {duration:2000});
        setTimeout(() =>{
          this.router.navigateByUrl('students');
        },2000)
      },

      error: (err) => {
        this.snackbar.open('Öğrenci silinemedi!',undefined, {duration:2000});
      }
    });
  }

  onAdd()
  {
    this.studentService.addStudent(this.student).subscribe({
      next: (success) => {
        setTimeout(() =>{
          this.router.navigateByUrl(`students/${success.id}`);
        },2000)
        
        this.snackbar.open('Öğrenci eklendi',undefined, {duration:2000});
    },

    error: (err) => {
      this.snackbar.open('Öğrenci eklenemedi!',undefined, {duration:2000});
    }

    });
  }

  setImage()
  {
    if(this.student.profileImageUrl)
    {
      this.displayProfileImageUrl = this.studentService.getImagePath(this.student.profileImageUrl);
    }
    else
    {
      this.displayProfileImageUrl = 'assets/user.png';
    }
  }

  uploadImage(event : any)
  {
    if(this.studentId)
    {
      const file : File = event.target.files[0];
      this.studentService.uploadImage(this.student.id,file).subscribe({
        next: (success) => {

          this.student.profileImageUrl = success;
          this.setImage();

          this.snackbar.open('Resim güncellendi',undefined, {duration:2000});
          
        },
  
        error: (err) => {
          this.snackbar.open('Öğrenci silinemedi!',undefined, {duration:2000});
        }
      });
    }
  }

}
