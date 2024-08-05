import { Component } from '@angular/core';
import { MatDialog } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Task } from "../tasks/task";
import { TasksService } from "../tasks/tasks.service";
import { Settings } from "../settings/settings";
import { SettingsService } from "../settings/settings.service";
import { ConfirmModal } from "../../shared/confirm-modal/confirm.modal";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  tasks: Task[] = [];
  settings: Settings;

  constructor(private tasksService: TasksService, private settingsService: SettingsService,
              private matSnackBar: MatSnackBar, private matDialog: MatDialog) {
    this.settingsService.get().subscribe(settings => this.settings = settings);
  }

  ngOnInit() {
    this.tasksService.getAll().subscribe(tasks => this.tasks = tasks);
  }

  taskDeleted(task: Task) {
    const idx = this.tasks.findIndex(t => t.id === task.id);
    this.tasks = this.tasks.filter(t => t.id !== task.id);
    const snackBar = this.matSnackBar.open("Task deleted", 'Undo', {duration: 3000});
    delete task.id;
    delete task.dateCreated;
    snackBar.onAction().subscribe(() => {
      this.tasksService.create(task).subscribe({
        next: task => this.tasks.splice(idx, 0, task),
        error: () => this.matSnackBar.open("Failed to restore task", '', {duration: 5000})
      });
    });
  }

  deleteAll() {
    const dialogRef = this.matDialog.open(ConfirmModal, {data: {title: 'Delete all tasks', message: 'Are you sure you want to delete all tasks?'}});
    dialogRef.afterClosed().subscribe(result => {
      if (!result) {
        return;
      }

      this.tasksService.deleteAll().subscribe({
        next: (() => {
          this.tasks = [];
          this.matSnackBar.open("Tasks deleted", '', {duration: 3000});
        }),
        error: (() => {
          this.matSnackBar.open("Failed to delete all tasks", '', {duration: 5000});
        })
      });
    });
  }
}
