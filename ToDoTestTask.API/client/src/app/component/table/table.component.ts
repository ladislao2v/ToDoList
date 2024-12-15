import { Component, OnInit } from '@angular/core';
import {ToDoTaskResponse} from "../../model/ToDoTaskResponse";
import {ToDoTaskService} from "../../service/to-do-task.service";
import {NotificationService} from "../../service/notification.service";
import {ToDoTasksResponse} from "../../model/ToDoTasksResponse";
import {ToDoTaskEditRequest} from "../../model/ToDoTaskEditRequest";
import {Router} from "@angular/router";

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {
  tasks!: ToDoTaskResponse[];
  isNotified: boolean = false
  constructor(private router: Router,
              private toDoTaskService: ToDoTaskService,
              private notificationService: NotificationService) {}

  ngOnInit() {
    this.loadTasksByCreateTime();
  }

  private loadTasksByCreateTime() {
    this.toDoTaskService.getAll().subscribe(
      (data : ToDoTasksResponse) => {
        this.tasks = data.tasks;

        if (this.isNotified)
          return

        this.tasks.forEach(task => {
            this.checkNotificationTo(task);
          })



        this.isNotified = true;
      },
      (error) => {
        console.error('Error occurred:', error);
        this.notificationService.sendError("Something went wrong, try again!")
      }
    );
  }

  private checkNotificationTo(task: ToDoTaskResponse) {
    if(task.isRunningOut)
      this.notificationService.sendNotification("\"" + task.title + "\" is about to expire!")

    if(task.days == 0 && task.hours < 24 && task.priority == "High")
      this.notificationService.sendWarning("\"" + task.title + "\" do it as soon as possible!")
  }

  public loadTasksByStatus() {
    this.toDoTaskService.getTasksSortedByStatus().subscribe(
      (data : ToDoTasksResponse) => {
        this.tasks = data.tasks;
      },
      (error) => {
        console.error('Error occurred:', error);
        this.notificationService.sendError("Something went wrong, try again!")
      }
    );
  }

  public loadTasksByPriority() {
    this.toDoTaskService.getTasksSortedByPriority().subscribe(
      (data : ToDoTasksResponse) => {
        this.tasks = data.tasks;
      },
      (error) => {
        console.error('Error occurred:', error);
        this.notificationService.sendError("Something went wrong, try again!")
      }
    );
  }

  public loadTasksByDueDate() {
    this.toDoTaskService.getTasksSortedByDueDate().subscribe(
      (data : ToDoTasksResponse) => {
        this.tasks = data.tasks;
      },
      (error) => {
        console.error('Error occurred:', error);
        this.notificationService.sendError("Something went wrong, try again!")
      }
    );
  }

  public onTaskChanged(request: ToDoTaskEditRequest) {
    this.toDoTaskService.edit(request.id, request)
      .subscribe(
      () => {
        this.loadTasksByCreateTime();
        this.notificationService.sendSuccess("Ok")
      },
      (error) => {
        console.error('Error occurred:', error);
        this.notificationService.sendError("Something went wrong, try again!")
      }
    );
  }

  public onTaskDeleted(id: number) {
    this.toDoTaskService.delete(id)
      .subscribe(
        () => {
          this.loadTasksByCreateTime();
        },
        (error) => {
          console.error('Error occurred:', error);
          this.notificationService.sendError("Something went wrong, try again!")
        }
      );
  }

  add() {
    this.router.navigate(["add"])
  }
}
