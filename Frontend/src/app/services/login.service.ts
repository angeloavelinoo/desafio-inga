import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { API_CONFIG } from '../Configs/API_CONFIG';
import { Observable } from 'rxjs';
import { MessageResponse } from '../../Models/message';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

    jwtService: JwtHelperService = new JwtHelperService();

    constructor(private http:HttpClient) { }

    authenticate(username:string, password:string): Observable<MessageResponse> {
        return this.http.post<MessageResponse>(`${API_CONFIG.baseUrl}/Authentication`, {username, password})
    }

    successfulLogin(authToken: string){
        sessionStorage.setItem('token', authToken)
    }

    isAuthenticated(){
      let token =  sessionStorage.getItem('token')

      if(token != null){
        return !this.jwtService.isTokenExpired(token)
      }
      return false
    }

    logout(){
        sessionStorage.clear()
    }
    
}
