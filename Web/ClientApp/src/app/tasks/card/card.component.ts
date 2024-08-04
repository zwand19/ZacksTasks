import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Task } from "../task";
import { TasksService } from "../tasks.service";

@Component({
  selector: 'app-task',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class ViewTaskComponent {
  @Input() task: Task;
  @Output() deleted = new EventEmitter();

  constructor(private tasksService: TasksService) {
  }

  delete() {
    this.tasksService.delete(this.task.id).subscribe(() => {
      this.deleted.emit();
    });
  }
}
