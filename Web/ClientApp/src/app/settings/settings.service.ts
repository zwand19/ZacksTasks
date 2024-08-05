import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, of, ReplaySubject } from "rxjs";
import { Settings } from "./settings";

@Injectable({providedIn: 'root'})
export class SettingsService {
  private settings: Settings;

  constructor(private http: HttpClient) {
  }

  get(): Observable<Settings> {
    if (this.settings) {
      return of(this.settings);
    }

    const obs = new ReplaySubject<Settings>();
    this.http.get<Settings>('api/v1/settings').subscribe({
      next: (settings => {
        this.settings = settings;
        obs.next(settings);
      }),
      error: (error => {
        obs.error(error);
      }),
      complete: (() => {
        obs.complete();
      })
    });
    return obs;
  }
}
