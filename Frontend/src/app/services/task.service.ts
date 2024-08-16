import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_CONFIG } from '../Configs/API_CONFIG';
import { MessageResponse } from '../../Models/message';
import {Observable} from 'rxjs';

@Injectable({
    providedIn: 'root'
  })
  export class TaskService {
  
  
  
  
    constructor(private http:HttpClient) { }
  
    getList(projectId:string): Observable<MessageResponse> {
        return this.http.get<MessageResponse>(`${API_CONFIG.baseUrl}/Tasks/List/${projectId}`)
    }

    create(projectId:string,collaboratorId:string,startTime:Date,endTime:Date, name:string, description:string): Observable<MessageResponse> {
      return this.http.post<MessageResponse>(`${API_CONFIG.baseUrl}/Tasks`, {projectId,collaboratorId,startTime,endTime,name,description})
    } 

    delete(id: string): Observable<MessageResponse> {
      return this.http.delete<MessageResponse>(`${API_CONFIG.baseUrl}/Tasks`, {
        params: { id }
      });
    }

    update(id: string,projectId:string,collaboratorId:string,startTime:Date,endTime:Date, name:string, description:string): Observable<MessageResponse> {
      return this.http.put<MessageResponse>(`${API_CONFIG.baseUrl}/Tasks/${id}`, {id, projectId, collaboratorId, startTime, endTime, name, description });
    } 

    get(id:string): Observable<MessageResponse> {
      return this.http.get<MessageResponse>(`${API_CONFIG.baseUrl}/Tasks`, {
        params: { id }
      });
    }
  }
  