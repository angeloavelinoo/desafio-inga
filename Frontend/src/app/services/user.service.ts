import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_CONFIG } from '../Configs/API_CONFIG';
import { MessageResponse } from '../../Models/message';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {




  constructor(private http:HttpClient) { }

  getList(): Observable<MessageResponse> {
      return this.http.get<MessageResponse>(`${API_CONFIG.baseUrl}/User/List`)
  }
}
