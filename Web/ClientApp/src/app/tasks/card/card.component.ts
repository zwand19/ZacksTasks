import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatDialog } from "@angular/material/dialog";
import { Task } from "../task";
import { TasksService } from "../tasks.service";
import { TaskModalComponent } from "../modal/modal.component";

@Component({
  selector: 'app-task',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class ViewTaskComponent {
  @Input() task: Task;
  @Output() deleted = new EventEmitter();

  constructor(private tasksService: TasksService, private matDialog: MatDialog) {
  }

  delete() {
    this.tasksService.delete(this.task.id!).subscribe(() => this.deleted.emit());
  }

  taskClicked() {
    this.matDialog.open(TaskModalComponent, {data: this.task});
  }
}
