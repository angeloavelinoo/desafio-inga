import { Component, inject } from '@angular/core';
import { User } from '../../interfaces/user';
import { ReactiveFormsModule, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SignUpService } from '../../services/signup.service';
import { ToastrService } from 'ngx-toastr';
import { MessageResponse } from '../../../Models/message';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss'
})
export class SignUpComponent {

  errorMessage:string = "";
  private formBuilderService = inject(FormBuilder)

  constructor(private signUpService: SignUpService, private router: Router, private toastr: ToastrService) {}

  protected form = this.formBuilderService.group({
    username: ['', [Validators.required, Validators.minLength(3)]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  create(username: string, password: string) {
    if (this.form.valid) {
      this.signUpService.create(username, password).subscribe({
        next: (response: MessageResponse) => {
          this.toastr.success('Usuário criado com sucesso');
          this.router.navigate(['/login']);
        },
        error: (errorResponse: HttpErrorResponse) => {
          this.errorMessage = errorResponse.error.message || 'Erro desconhecido';
          this.toastr.error(this.errorMessage);
        }
      });
    } else {
      this.form.markAllAsTouched();  
    }
  }

}

