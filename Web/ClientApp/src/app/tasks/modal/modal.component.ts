import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Task } from "../task";

@Component({
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class TaskModalComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public task: Task) {
  }
}
