import { Component,  OnInit, ViewChild } from '@angular/core';
import { ProjectsService } from '../../services/projects.service';
import { MessageResponse } from '../../../Models/message';
import { Project } from '../../interfaces/project';
import { HttpErrorResponse } from '@angular/common/http';
import {ToastrService} from 'ngx-toastr';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';


@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrl: './projects.component.scss'
})
export class ProjectsComponent {

  displayedColumns: string[] = ['name', 'actions'];
  dataSource: MatTableDataSource<Project> = new MatTableDataSource<Project>();
  isMenuVisible = false;


  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private projectService: ProjectsService, private toastr: ToastrService) {

  }

  ngOnInit() {
    this.getProjects();
  }

  toggleMenu() {
    this.isMenuVisible = !this.isMenuVisible;
  }

  getProjects() {
    this.projectService.getList().subscribe({
      next:(response: MessageResponse) =>{
        this.dataSource.data = response.data;   
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      error: (valorErro: HttpErrorResponse) => {
        this.toastr.error = valorErro.error.message;
      }
    })
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }



  viewProject(row: any) {
    // Lógica para visualizar o projeto
    console.log('Visualizando projeto:', row);
  }

  deleteProject(row: Project) {
    this.projectService.delete(row.id).subscribe({
        next: (response: MessageResponse) => {
          this.toastr.success('Projeto deletado com sucesso');
          this.getProjects();
        },
        error: (errorResponse: HttpErrorResponse) => {
          this.toastr.error(errorResponse.error.message);
      }
    })
  }


}
