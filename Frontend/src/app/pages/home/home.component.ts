import { Component } from '@angular/core';
import { User } from '../../interfaces/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  userName: string | null;

  constructor(private router : Router) {}

  ngOnInit() {
    this.userName = sessionStorage.getItem('username');
  
    if(sessionStorage.getItem('token') === null) {
      this.router.navigate(['/login']);
    }
  }

}
