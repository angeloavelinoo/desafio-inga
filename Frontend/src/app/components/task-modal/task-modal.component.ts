import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Task } from '../../interfaces/task';
import { User } from '../../interfaces/user';
import { UserService } from '../../services/user.service';
import { MessageResponse } from '../../../Models/message';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-task-modal',
  templateUrl: './task-modal.component.html',
  styleUrls: ['./task-modal.component.scss']
})
export class TaskModalComponent implements OnInit {
  taskForm!: FormGroup;
  isEditMode = false;
  users: User[] = []; // Lista de usuários

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<TaskModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Task | null,
    private userService : UserService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.taskForm = this.fb.group({
      name: [this.data?.name || '', [Validators.required, Validators.minLength(3)]],
      description: [this.data?.description || '', Validators.required],
      startDate: [this.data?.startDate ? this.convertToDate(this.data.startDate).toISOString().split('T')[0] : '', Validators.required],
      startTime: [this.data?.startDate ? this.convertToDate(this.data.startDate).toTimeString().split(' ')[0].substring(0, 5) : '', Validators.required],
      user: [this.data?.collaboratorName || null],
      endTime: [this.data?.endDate ? this.convertToDate(this.data.endDate).toTimeString().split(' ')[0].substring(0, 5) : '', Validators.required],
    });

    if (this.data) {
      this.isEditMode = true;
    }
     this.getCollaborators();

  }

  getCollaborators() {
    this.userService.getList().subscribe({
      next:(response: MessageResponse) =>{
        this.users = response.data;   

      },
      error: (valorErro: HttpErrorResponse) => {
        this.toastr.error = valorErro.error.message;
      }
    })
  }

  save(): void {
    if (this.taskForm.valid) {
      this.dialogRef.close(this.taskForm.value);
    }
  }

  cancel(): void {
    this.dialogRef.close(null);
  }


  convertToDate(dateStr: string): Date {
    const [day, month, year, hour, minute] = dateStr.split(/[\/\s:]/);
    return new Date(
        +year,         
        +month - 1,     
        +day,          
        +hour,          
        +minute        
    );
}
}
