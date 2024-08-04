import { Component, EventEmitter, Output } from '@angular/core';
import { Task } from "../task";
import { TasksService } from "../tasks.service";
import { MatSnackBar } from "@angular/material/snack-bar";

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.scss']
})
export class CreateTaskComponent {
  @Output() created = new EventEmitter<Task>();
  description: string;
  saving: boolean;

  constructor(private tasksService: TasksService, private matSnackBar: MatSnackBar) {
  }

  save() {
    const task = new Task();
    task.description = this.description;
    this.saving = true;
    this.tasksService.create(task).subscribe({
      next: (task => {
        this.description = '';
        this.saving = false;
        this.created.emit(task);
      }),
      error: (() => {
        this.saving = false;
        this.matSnackBar.open("Failed to create task");
      })
    });
  }

  isValidDescription(): boolean {
    // whatever rules we may have for a valid description
    return !!this.description && this.description.trim().length > 2;
  }
}
