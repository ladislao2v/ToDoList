import {Component, Input, Output, EventEmitter, OnInit} from '@angular/core';
import {ToDoTaskResponse} from "../../model/ToDoTaskResponse";
import {ToDoTaskEditRequest} from "../../model/ToDoTaskEditRequest";
import {DateService} from "../../service/date-service";

@Component({
  selector: 'app-task-card',
  templateUrl: './task-card.component.html',
  styleUrls: ['./task-card.component.css']
})
export class TaskCardComponent implements OnInit{
  constructor(private dateService : DateService) {
  }

  @Input() task!: ToDoTaskResponse

  @Output() saved = new EventEmitter<ToDoTaskEditRequest>();
  @Output() statusChanged = new EventEmitter<ToDoTaskEditRequest>();
  @Output() priorityChanged = new EventEmitter<ToDoTaskEditRequest>();
  @Output() deleted = new EventEmitter<number>();

  isEditMode: boolean = false;
  editIcon: string = 'edit';

  editableTitle!: string;
  editableDescription!: string;
  editableDueDate!: string;

  ngOnInit() {
    if (this.task) {
      this.editableTitle = this.task.title;
      this.editableDescription = this.task.description;
      this.editableDueDate = this.task.dueDate;
    }
  }

  toggleEditMode() {
    this.isEditMode = !this.isEditMode;
    this.editIcon = this.isEditMode ? 'check' : 'edit';
    if (!this.isEditMode) {
      this.saveChanges();
    }
  }

  saveChanges() {
    this.editableDueDate = this.dateService.convertToCustomFormat(this.editableDueDate)

    if (this.editableDueDate == "NaN.NaN.NaN NaN:NaN")
      this.editableDueDate = this.task.dueDate

    this.saved.emit({
      id: this.task.id,
      title: this.editableTitle,
      description: this.editableDescription,
      dueDate:  this.editableDueDate,
      priority: this.task.priority,
      status: this.task.status
    });
  }

  onStatusChanged(newStatus: string) {
    this.statusChanged.emit({
        id: this.task.id,
        title: this.task.title,
        description: this.task.description,
        dueDate: this.task.dueDate,
        priority: this.task.priority,
        status: newStatus.toLowerCase()
      });
  }

  onPriorityChanged(newPriority: string) {
    this.priorityChanged.emit({
      id: this.task.id,
      title: this.task.title,
      description: this.task.description,
      dueDate: this.task.dueDate,
      priority: newPriority.toLowerCase(),
      status: this.task.status
    });
  }

  delete() {
    this.deleted.emit(this.task.id)
  }

  getRemainingTime() {
    if(this.task.days <= 0 && this.task.hours == 0)
      return "<1 h."
    else if(this.task.days <= 0 && this.task.hours < 0)
      return "Expired"
    else
      return this.task.days + ' d. ' + this.task.hours + ' h.'
  }
}
