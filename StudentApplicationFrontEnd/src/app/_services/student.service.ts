import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { map, catchError } from 'rxjs/operators';
import { throwError } from 'rxjs/internal/observable/throwError';
import { Observable } from 'rxjs';
import { Student } from '../_interfaces/student';
import { NotificationService } from './notification.service';
@Injectable({
  providedIn: 'root',
})
export class StudentService {
  serverUrl = environment.baseUrl;
  baseUrl = environment.baseUrl + '/api/student';
  constructor(
    private http: HttpClient,
    private notificationService: NotificationService
  ) {}

  public list(
    keyword: string = '',
    pageNumber: number = 1,
    pageSize: number = 10
  ): Observable<any> {
    const fullurl =
      this.baseUrl +
      '/list?keyword=' +
      keyword +
      '&pageNo=' +
      pageNumber +
      '&pageSize=' +
      pageSize;

    return this.http.get(fullurl).pipe(
      map((response: any) => {
        console.log('list : ' + fullurl, response);
        return response.data;
      }),
      catchError((error) => {
        return throwError(error);
      })
    );
  }
  public delete(id: number): Observable<Student> {
    return this.http.delete(this.baseUrl + '/delete/' + id).pipe(
      map((response: any) => {
        return response.data;
      }),
      catchError((error) => {
        return throwError(error);
      })
    );
  }
  public add(stu: Student): Observable<Student> {
    return this.http.post(this.baseUrl + '/add', stu).pipe(
      map((response: any) => {
        return response.data;
      }),
      catchError((error) => {
        return throwError(error);
      })
    ); // end of pipe
  }
  public update(stu: Student): Observable<Student> {
    return this.http.put(this.baseUrl + '/update', stu).pipe(
      map((response: any) => {
        return response.data;
      }),
      catchError((error) => {
        return throwError(error);
      })
    ); // end of pipe
  }
}
