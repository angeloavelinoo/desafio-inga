import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API_CONFIG } from '../Configs/API_CONFIG';
import { Observable } from 'rxjs';
import { MessageResponse } from '../../Models/message';

@Injectable({
    providedIn: 'root'
  })


export class SignUpService{


  constructor(private http:HttpClient) { }

  create(username:string, password:string): Observable<MessageResponse> {
      return this.http.post<MessageResponse>(`${API_CONFIG.baseUrl}/User`, {username, password})
  }

    
}