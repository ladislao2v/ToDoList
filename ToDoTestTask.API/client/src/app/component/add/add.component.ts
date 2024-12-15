import {Component} from '@angular/core';
import {NotificationService} from "../../service/notification.service";
import {Router} from "@angular/router";
import {ToDoTaskService} from "../../service/to-do-task.service";
import {DateService} from "../../service/date-service";

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrl: './add.component.css'
})
export class AddComponent{
  title!: string;
  description!: string;
  dueDate!: string;
  priority!: string;

  constructor(
    private notificationService: NotificationService,
    private toDoTaskService: ToDoTaskService,
    protected router: Router,
    private dateService : DateService
  ) {
  }

  onPriorityChanged(newPriority: string) {
    this.priority = newPriority;
  }
  submit(){
    this.dueDate = this.dateService.convertToCustomFormat(this.dueDate)

    if(!this.tryValidate(this.title, this.description, this.dueDate, this.priority))
    {
      this.notificationService.sendError("Invalid data");
      return;
    }

    this.toDoTaskService.add({
      title: this.title,
      description: this.description,
      dueDate: this.dueDate,
      priority: this.priority
    }).subscribe(_ => {
      this.notificationService.sendSuccess("Ok")
      this.router.navigate(["tasks"])
    }, error=> {
      this.notificationService.sendError("Something went wrong, try again!")
    });
  }

  protected tryValidate(title: string, description: string, dueDate: string, priority: string) {
    if (this.title == "")
      return false

    if (this.description == "")
      return false

    if (this.dueDate == "" || this.dueDate == "NaN.NaN.NaN NaN:NaN")
      return false

    if (this.priority == "")
      return false

    return true;
  }
}
