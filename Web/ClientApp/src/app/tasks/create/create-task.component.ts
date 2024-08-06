import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatSnackBar } from "@angular/material/snack-bar";
import { Task } from "../task";
import { TasksService } from "../tasks.service";

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.scss']
})
export class CreateTaskComponent {
  @Output() created = new EventEmitter<Task>();
  @Input() useAI: boolean;
  description: string;
  saving: boolean;

  constructor(private tasksService: TasksService, private matSnackBar: MatSnackBar) {
  }

  save() {
    const task = new Task();
    task.description = this.description;
    this.saving = true;
    if (this.useAI) {
      this.tasksService.createSubTasksFromAI(task).subscribe({
        next: (tasks => {
          this.description = '';
          this.saving = false;
          tasks.forEach(t => this.created.emit(t));
        }),
        error: (() => {
          this.saving = false;
          this.matSnackBar.open("Failed to create sub-tasks", '', {duration: 5000});
        })
      });
    } else {
      this.tasksService.create(task).subscribe({
        next: (task => {
          this.description = '';
          this.saving = false;
          this.created.emit(task);
        }),
        error: (() => {
          this.saving = false;
          this.matSnackBar.open("Failed to create task", '', {duration: 5000});
        })
      });
    }
  }

  isValidDescription(): boolean {
    // whatever rules we may have for a valid description
    // TODO: unit tests
    return !!this.description && this.description.trim().length >= 2 && this.description.trim().length <= 1000;
  }
}
