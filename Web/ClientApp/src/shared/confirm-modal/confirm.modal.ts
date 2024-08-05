import { Component, Inject } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";

@Component({
  templateUrl: './confirm.modal.html',
  styleUrls: ['./confirm.modal.scss']
})
export class ConfirmModal {
  title: string;
  message: string;

  constructor(public dialogRef: MatDialogRef<ConfirmModal>, @Inject(MAT_DIALOG_DATA) data: ConfirmModal) {
    this.title = data.title;
    this.message = data.message;
  }
}
