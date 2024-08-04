import { Component } from '@angular/core';
import { TimeagoIntl } from "ngx-timeago";
import { strings as englishStrings } from "ngx-timeago/language-strings/en";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';

  constructor(intl: TimeagoIntl) {
    intl.strings = englishStrings;
    intl.changes.next();
  }
}
