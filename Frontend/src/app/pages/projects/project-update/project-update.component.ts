import { Component, inject } from '@angular/core';
import { ReactiveFormsModule, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ProjectsService } from '../../../services/projects.service';
import { ToastrService } from 'ngx-toastr';
import { MessageResponse } from '../../../../Models/message';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-project-update',
  templateUrl: './project-update.component.html',
  styleUrl: './project-update.component.scss'
})
export class ProjectUpdateComponent {

  projectId!: string;
  errorMessage:string = "";
  private formBuilderService = inject(FormBuilder)
  constructor(private projectService: ProjectsService, private toastr: ToastrService,private route: ActivatedRoute, private router: Router) {}

  protected form = this.formBuilderService.group({
    name: ['', [Validators.required, Validators.minLength(3)]]
  });



  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.projectId = params.get('id')!;
      this.loadProject();
    });
  }

  loadProject() {
    this.projectService.get(this.projectId).subscribe({
      next: project => {
        this.form.patchValue({ name: project.data.name });
      },
      error: (errorResponse: HttpErrorResponse) => {
        this.toastr.error(errorResponse.error.message);
      }
    });
  }


  update(name:string) {
    this.projectService.update(this.projectId, name).subscribe({
        next: (response: MessageResponse) => {
          this.toastr.success('Projeto atualizado com sucesso');
          this.router.navigate(['/projetos']);
        },
        error: (errorResponse: HttpErrorResponse) => {
          this.toastr.error(errorResponse.error.message);
      }
    })
  }

}

