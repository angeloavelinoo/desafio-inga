import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_CONFIG } from '../Configs/API_CONFIG';
import { MessageResponse } from '../../Models/message';
import {Observable} from 'rxjs';

@Injectable({
    providedIn: 'root'
  })
  export class ProjectsService {
  
  
  
  
    constructor(private http:HttpClient) { }
  
    getList(): Observable<MessageResponse> {
        return this.http.get<MessageResponse>(`${API_CONFIG.baseUrl}/Projects/List`)
    }

    create(name:string): Observable<MessageResponse> {
      return this.http.post<MessageResponse>(`${API_CONFIG.baseUrl}/Projects`, {name})
    } 

    delete(id: string): Observable<MessageResponse> {
      return this.http.delete<MessageResponse>(`${API_CONFIG.baseUrl}/Projects`, {
        params: { id }
      });
    }

    update(id:string, name:string): Observable<MessageResponse> {
      return this.http.put<MessageResponse>(`${API_CONFIG.baseUrl}/Projects/${id}`, { name });
    } 

    get(id:string): Observable<MessageResponse> {
      return this.http.get<MessageResponse>(`${API_CONFIG.baseUrl}/Projects`, {
        params: { id }
      });
    }
  }
  