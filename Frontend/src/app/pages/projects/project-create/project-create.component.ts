import { Component, inject } from '@angular/core';
import { ReactiveFormsModule, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ProjectsService } from '../../../services/projects.service';
import { ToastrService } from 'ngx-toastr';
import { MessageResponse } from '../../../../Models/message';
import { HttpErrorResponse } from '@angular/common/http';
import {Router} from '@angular/router';

@Component({
  selector: 'app-project-create',
  templateUrl: './project-create.component.html',
  styleUrl: './project-create.component.scss'
})
export class ProjectCreateComponent {


  errorMessage:string = "";
  private formBuilderService = inject(FormBuilder)

  constructor(private projectService: ProjectsService, private toastr: ToastrService, private route: Router) {}

  protected form = this.formBuilderService.group({
    name: ['', [Validators.required, Validators.minLength(3)]]
  });

  create(name: string) {
    if (this.form.valid) {
      this.projectService.create(name).subscribe({
        next: (response: MessageResponse) => {
          this.toastr.success('Projeto criado com sucesso');
          this.route.navigate(['/projetos']);
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
