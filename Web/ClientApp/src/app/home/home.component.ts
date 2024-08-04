import { Component } from '@angular/core';
import { Task } from "../tasks/task";
import { TasksService } from "../tasks/tasks.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  tasks: Task[] = [];

  constructor(private tasksService: TasksService) {
  }

  ngOnInit() {
    this.tasksService.getAll().subscribe(tasks => this.tasks = tasks);
  }

  taskDeleted(task: Task) {
    this.tasks = this.tasks.filter(t => t.id !== task.id);
  }
}