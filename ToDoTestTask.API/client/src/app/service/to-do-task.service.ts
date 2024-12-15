import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {Constants} from "../constants/Constants";
import {ToDoTaskResponse} from "../model/ToDoTaskResponse";
import {ToDoTasksResponse} from "../model/ToDoTasksResponse";
import {ToDoTaskCreateRequest} from "../model/ToDoTaskCreateRequest";
import {ToDoTaskEditRequest} from "../model/ToDoTaskEditRequest";

@Injectable({
  providedIn: 'root'
})
export class ToDoTaskService {
  constructor(private  http: HttpClient) {}

  public getAll(){
    return this.http.get<ToDoTasksResponse>(Constants.TODOTASK_API)
  }

  public getTasksSortedByDueDate() {
    const params = new HttpParams().set('SortType', 'duedate');
    return this.http.get<ToDoTasksResponse>(Constants.TODOTASK_API, { params });
  }

  public getTasksSortedByStatus() {
    const params = new HttpParams().set('SortType', 'status');
    return this.http.get<ToDoTasksResponse>(Constants.TODOTASK_API, { params });
  }

  public getTasksSortedByPriority() {
    const params = new HttpParams().set('SortType', 'priority');
    return this.http.get<ToDoTasksResponse>(Constants.TODOTASK_API, { params });
  }

  public getById(id: number){
    return this.http.get<ToDoTaskResponse>(Constants.TODOTASK_API+"/"+id);
  }

  public add(task:ToDoTaskCreateRequest){
    return this.http.post(Constants.TODOTASK_API, {
      title: task.title,
      description: task.description,
      dueDate: task.dueDate,
      priority: task.priority
    });
  }

  public delete(id: number){
    return this.http.delete(Constants.TODOTASK_API+"/"+id)
  }

  public edit(id: number, task: ToDoTaskEditRequest) {
    return this.http.put(Constants.TODOTASK_API + "/" + id, {
      title: task.title,
      description: task.description,
      dueDate: task.dueDate,
      priority: task.priority,
      status: task.status
    });
  }
}
