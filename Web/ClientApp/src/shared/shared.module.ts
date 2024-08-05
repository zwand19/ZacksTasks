import { NgModule } from '@angular/core';
import { ConfirmModal } from "./confirm-modal/confirm.modal";
import { MaterialModule } from "./material.module";

@NgModule({
  declarations: [
    ConfirmModal
  ],
  imports: [MaterialModule],
  exports: [
    MaterialModule,
    ConfirmModal,
  ],
  providers: []
})
export class SharedModule { }
