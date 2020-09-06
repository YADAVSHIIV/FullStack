import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { map, catchError } from 'rxjs/operators';
import { throwError } from 'rxjs/internal/observable/throwError';
import { Observable } from 'rxjs';
import { Student } from '../_interfaces/student';
import { NotificationService } from './notification.service';
import { Mark } from '../_interfaces/mark';
@Injectable({
  providedIn: 'root',
})
export class MarkService {
  serverUrl = environment.baseUrl;
  baseUrl = environment.baseUrl + '/api/mark';
  constructor(
    private http: HttpClient,
    private notificationService: NotificationService
  ) {}

  public delete(id: number): Observable<Mark> {
    return this.http.delete(this.baseUrl + '/delete/' + id).pipe(
      map((response: any) => {
        return response.data;
      }),
      catchError((error) => {
        return throwError(error);
      })
    );
  }
  public add(mark: Mark): Observable<Mark> {
    return this.http.post(this.baseUrl + '/add', mark).pipe(
      map((response: any) => {
        return response.data;
      }),
      catchError((error) => {
        return throwError(error);
      })
    ); // end of pipe
  }
  public update(mark: Mark): Observable<Mark> {
    return this.http.put(this.baseUrl + '/update', mark).pipe(
      map((response: any) => {
        return response.data;
      }),
      catchError((error) => {
        return throwError(error);
      })
    ); // end of pipe
  }
}
