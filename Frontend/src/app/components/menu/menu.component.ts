import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss'
})
export class MenuComponent {

  isMenuVisible = false;

  constructor(private router: Router) {}

  toggleMenu(event: MouseEvent) {
    event.stopPropagation();
    this.isMenuVisible = !this.isMenuVisible;
  }

  logout() {
    sessionStorage.removeItem('token'); 
    sessionStorage.removeItem('username'); 
    this.router.navigate(['/login']);
  }
  
}
