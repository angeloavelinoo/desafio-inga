import { Component } from '@angular/core';
import { LoginService } from '../../services/login.service';
import { Router } from '@angular/router';
import { MessageResponse } from '../../../Models/message';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  name:string = "";
  senha:string = "";
  errorMessage:string = "";

  constructor(private loginService:LoginService, private router:Router, private toastr: ToastrService) { 

  }

  login(name : string, senha : string){
    this.loginService.authenticate(name, senha).subscribe({
      next: (valor: MessageResponse) =>{
        this.loginService.successfulLogin(valor.data.token)
        this.router.navigate(['/home'])
        sessionStorage.setItem("username", name)
      },
      error: (valorErro: MessageResponse) => {
        this.errorMessage = ("Usuário e/ou senha inválidos");
      }
    });
  }
}
