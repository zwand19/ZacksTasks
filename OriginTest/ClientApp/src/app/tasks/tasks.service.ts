import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {Task} from "./task";

@Injectable({providedIn: 'root'})
export class TasksService {
  getTasks(): Observable<Task[]> {
    return new Observable<Task[]>(observer => {
      observer.next([
        {id: 1, dateCreated: '', description: "Task 1"},
        {id: 2, dateCreated: '', description: "Task 2"},
        {id: 3, dateCreated: '', description: "Task 3"}
      ]);
      observer.complete();
    });
  }
}
