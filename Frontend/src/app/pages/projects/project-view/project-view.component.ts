import { Component, ViewChild } from '@angular/core';
import { ProjectsService } from '../../../services/projects.service';
import { TaskService } from '../../../services/task.service';	
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Task } from '../../../interfaces/task';
import { MessageResponse } from '../../../../Models/message';
import { TaskModalComponent } from '../../../components/task-modal/task-modal.component';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { CollaboratorService } from '../../../services/collaborator.service.';
import { User } from '../../../interfaces/user';
import { Collaborator } from '../../../interfaces/user copy';


@Component({
  selector: 'app-project-view',
  templateUrl: './project-view.component.html',
  styleUrl: './project-view.component.scss'
})
export class ProjectViewComponent {
  errorMessage:string = "";
  projectId!: string;
  projectName: string;
  collaboratorId: string;
  createdAt: string;
  updatedAt: string;
  tasks : Task[] = [];
  collaborators: Collaborator[] = [];
  selectedUser?: Collaborator;
  organizedTasks: { date: Date, tasks: Task[] }[] = [];
  dailyHours: { date: string, hours: number }[] = [];
  totalMonthlyHours: number = 0;


  displayedColumns: string[] = ['name' ,'collaborator', 'actions'];
  dataSource: MatTableDataSource<Task> = new MatTableDataSource<Task>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private projectService: ProjectsService, private toastr: ToastrService,private route: ActivatedRoute, private router: Router, private taskService: TaskService
    , private dialog: MatDialog, private collaboratorService: CollaboratorService
  ) {}

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.projectId = params.get('id')!;
      this.loadProject();
      this.loadTasks();
      this.loadUsers();

      
    });
  }

  loadProject() {
    this.projectService.get(this.projectId).subscribe({
      next: project => {
        this.projectName = project.data.name ;
        this.createdAt = project.data.createdAt ;
        this.updatedAt = project.data.updatedAt ;
      },
      error: (errorResponse: HttpErrorResponse) => {
        this.toastr.error(errorResponse.error.message);
      }
    });
  }

  calculateHours() {
    const dailyMap = new Map<string, number>();
    let totalHours = 0;

    this.tasks.forEach(task => {
      const startDateTime = new Date(this.convertToDate((task.startDate)));
      const endDateTime = new Date(this.convertToDate(task.endDate));
      const duration = (endDateTime.getTime() - startDateTime.getTime()) / (1000 * 60 * 60); // hours

      const dateString = `${startDateTime.getFullYear()}-${startDateTime.getMonth() + 1}-${startDateTime.getDate()}`;
      if (!dailyMap.has(dateString)) {
        dailyMap.set(dateString, 0);
      }
      dailyMap.set(dateString, dailyMap.get(dateString)! + duration);
      totalHours += duration;
    });

    this.dailyHours = Array.from(dailyMap.entries()).map(([date, hours]) => ({ date, hours }));
    this.totalMonthlyHours = this.calculateMonthlyHours();
  }

  calculateMonthlyHours(): number {
    const now = new Date();
    const startOfMonth = new Date(now.getFullYear(), now.getMonth(), 1);
    const endOfMonth = new Date(now.getFullYear(), now.getMonth() + 1, 0);

    return this.tasks
      .filter(task => {
        const startDate = new Date(this.convertToDate(task.startDate));
        return startDate >= startOfMonth && startDate <= endOfMonth;
      })
      .reduce((total, task) => {
        const startDateTime = new Date(this.convertToDate(task.startDate));
        const endDateTime = new Date(this.convertToDate(task.endDate));
        const duration = (endDateTime.getTime() - startDateTime.getTime()) / (1000 * 60 * 60); // hours
        return total + duration;
      }, 0);
  }

  organizeTasksByDate() {
    const taskMap = new Map<string, Task[]>();

    this.tasks.forEach(task => {
      const date = new Date(task.startDate);
      const dateString = `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;

      if (!taskMap.has(dateString)) {
        taskMap.set(dateString, []);
      }

      taskMap.get(dateString)?.push(task);
    });

    this.organizedTasks = Array.from(taskMap.entries()).map(([date, tasks]) => ({
      date: new Date(date),
      tasks
    })).sort((a, b) => a.date.getTime() - b.date.getTime());

    this.calculateHours();
  }



  createTask(name: string, description: string,collaboratorName:string, startDate: Date, startTime: string, endTime: string) {
    const startDateTime = new Date(startDate.toISOString().split('T')[0] + 'T' + startTime);
    const endDateTime = new Date(startDate.toISOString().split('T')[0] + 'T' + endTime);
    
    if(collaboratorName){
      this.selectedUser = this.collaborators.find(c => c.name === collaboratorName);
      console.log(this.selectedUser);
      if (this.selectedUser) {
        this.collaboratorId = this.selectedUser.id;
      }
    }
   

    this.taskService.create(this.projectId, this.collaboratorId, startDateTime, endDateTime, name, description).subscribe({
      next: (response: MessageResponse) => {
        this.toastr.success('Tarefa criada com sucesso');
        this.loadTasks();
      },
      error: (errorResponse: HttpErrorResponse) => {
        this.toastr.error(errorResponse.error.message);
      }
    });
  }

  editTask(id:string, name: string, description: string, collaboratorName:string, startDate: Date, startTime: string, endTime: string) {
    const startDateTime = new Date(startDate + 'T' + startTime);
    const endDateTime = new Date(startDate + 'T' + endTime);
    
    if(collaboratorName){
      this.selectedUser = this.collaborators.find(c => c.name === collaboratorName);
      console.log(this.selectedUser);
      if (this.selectedUser) {
        this.collaboratorId = this.selectedUser.id;
      }
    }


    this.taskService.update(id,this.projectId, this.collaboratorId, startDateTime, endDateTime, name, description).subscribe({

      next: (response: MessageResponse) => {
        this.toastr.success('Tarefa atualizada com sucesso');
        this.loadTasks();
      },
      error: (errorResponse: HttpErrorResponse) => {
        this.toastr.error(errorResponse.error.message);
      }
    });
  }

  deleteTask(id: string) {
    this.taskService.delete(id).subscribe({
      next: () => {
        this.toastr.success('Tarefa excluída com sucesso');
        this.loadTasks(); 
      },
      error: (err) => this.toastr.error('Erro ao excluir tarefa')
    });
  }


  loadTasks() {
    this.taskService.getList(this.projectId).subscribe({
      next: (response: MessageResponse) => {
        this.tasks = response.data;
        this.dataSource = new MatTableDataSource(this.tasks);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.organizeTasksByDate();
      }
    });
  }

  loadUsers() {
    this.collaboratorService.getList().subscribe({
      next:(response: MessageResponse) =>{
        this.collaborators = response.data;   
      },
      error: (valorErro: HttpErrorResponse) => {
        this.toastr.error = valorErro.error.message;
      }
    })
  }

  openTaskModal(task?: Task): void {
    const dialogRef = this.dialog.open(TaskModalComponent, {
      width: '600px',
      data: task || null
    });

    console.log(task);
  
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (task) {
          this.editTask(task.id,result.name, result.description, result.user, result.startDate, result.startTime, result.endTime);
        } else {
          this.createTask(result.name, result.description, result.user, result.startDate, result.startTime, result.endTime);

        }
      }
    });
  }  
  

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
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
