import { Component,  OnInit, ViewChild } from '@angular/core';
import { UserService } from '../../services/user.service';
import { MessageResponse } from '../../../Models/message';
import { User } from '../../interfaces/user';
import { HttpErrorResponse } from '@angular/common/http';
import {ToastrService} from 'ngx-toastr';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-collaborators',
  templateUrl: './collaborators.component.html',
  styleUrl: './collaborators.component.scss'
})
export class CollaboratorsComponent  {

  displayedColumns: string[] = ['username'];
  dataSource: MatTableDataSource<User> = new MatTableDataSource<User>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private userService: UserService, private toastr: ToastrService) {

  }

  ngOnInit() {
    this.getCollaborators();
  }

  getCollaborators() {
    this.userService.getList().subscribe({
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

}
