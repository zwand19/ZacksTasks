import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { CreateTaskComponent } from "./tasks/create/create-task.component";
import { ViewTaskComponent } from "./tasks/card/card.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { SharedModule } from "../shared/shared.module";
import { TimeagoCustomFormatter, TimeagoFormatter, TimeagoIntl, TimeagoModule } from "ngx-timeago";
import { TaskModalComponent } from "./tasks/modal/modal.component";

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CreateTaskComponent,
    ViewTaskComponent,
    TaskModalComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    SharedModule,
    TimeagoModule.forRoot({
      intl: { provide: TimeagoIntl, useClass: TimeagoIntl },
      formatter: { provide: TimeagoFormatter, useClass: TimeagoCustomFormatter },
    }),
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
