import {Component, Input} from '@angular/core';
import {Task} from "./task";

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})
export class ViewTaskComponent {
  @Input() task: Task;
}
