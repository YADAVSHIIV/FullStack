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
export class QueryService {
  serverUrl = environment.baseUrl;
  baseUrl = environment.baseUrl + '/api/query';
  constructor(
    private http: HttpClient,
    private notificationService: NotificationService
  ) {}

  public list(
    keyword: string = '',
    filterType: number = 0,
    pageNumber: number = 1,
    pageSize: number = 10
  ): Observable<any> {
    const fullurl =
      this.baseUrl +
      '/list?keyword=' +
      keyword +
      '&filterType=' +
      filterType +
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
}
