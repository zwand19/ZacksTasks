import {Injectable} from "@angular/core";
import { Observable, ReplaySubject } from "rxjs";
import {Task} from "./task";
import {HttpClient} from "@angular/common/http";

@Injectable({providedIn: 'root'})
export class TasksService {
  constructor(private http: HttpClient) {
  }

  getAll(): Observable<Task[]> {
    const subject = new ReplaySubject<Task[]>();
    this.http.get<Task[]>('api/v1/tasks').subscribe({
      next: (tasks => {
        tasks.forEach(task => {
          console.log('Before: ' + task.dateCreated);
          task.dateCreated = new Date(task.dateCreated + 'Z').toLocaleString();
          console.log('After: ' + task.dateCreated);
        });
        subject.next(tasks);
      }),
      error: (error => {
        subject.error(error);
      }),
      complete: (() => {
        subject.complete();
      })
    });
    return subject;
  }

  create(task: Task): Observable<Task> {
    return this.http.post<Task>('api/v1/tasks', task);
  }

  delete(id: number): Observable<Object> {
    return this.http.delete(`api/v1/tasks/${id}`);
  }
}
