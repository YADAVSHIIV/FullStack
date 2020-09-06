import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { map, catchError } from 'rxjs/operators';
import { throwError } from 'rxjs/internal/observable/throwError';
import { Observable } from 'rxjs';
import { Subject } from '../_interfaces/subject';
import { NotificationService } from './notification.service';
@Injectable({
  providedIn: 'root',
})
export class SubjectService {
  serverUrl = environment.baseUrl;
  baseUrl = environment.baseUrl + '/api/subject';
  constructor(
    private http: HttpClient,
    private notificationService: NotificationService
  ) {}

  public getAll(): Observable<Subject[]> {
    return this.http.get(this.baseUrl + '/get').pipe(
      map((response: any) => {
        console.log(response);
        return response.data;
      }),
      catchError((error) => {
        return throwError(error);
        // return this.notificationService.showFromError(error);
      })
    );
  }

  public delete(id: number): Observable<Subject> {
    return this.http.delete(this.baseUrl + '/delete/' + id).pipe(
      map((response: any) => {
        return response.data;
      }),
      catchError((error) => {
        return throwError(error);
      })
    );
  }
  public add(sub: Subject): Observable<Subject> {
    return this.http.post(this.baseUrl + '/add', sub).pipe(
      map((response: any) => {
        return response.data;
      }),
      catchError((error) => {
        return throwError(error);
      })
    ); // end of pipe
  }
}
