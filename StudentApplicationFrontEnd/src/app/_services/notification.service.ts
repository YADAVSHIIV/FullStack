import { Injectable } from '@angular/core';
import { Ng2IzitoastService } from 'ng2-izitoast';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  constructor(public iziToast: Ng2IzitoastService) {}

  showSuccess(
    message: string,
    title: string = '',
    timeout: number = 5000
  ): void {
    alert(message);
    //  this.iziToast.success({ title, message, timeout });
    // this.tosterService.success(message, title, { timeOut: timeout });
  }

  showError(message: string, title: string = '', timeout: number = 5000): void {
    alert(message);
    // this.iziToast.error({ title, message, timeout });
    // this.tosterService.error(message, title, { timeOut: timeout });
  }
  showFromError(error: Error | HttpErrorResponse): void {
    if (error instanceof HttpErrorResponse) {
      if (error.status === 0) {
        this.showError('', 'no response from server');
      } else if (error.status === 404) {
        this.showError('', 'error 404 : not found.');
      } else if (!error.error.data) {
        this.showError('', error.error.message);
      } else {
        this.showError(error.error.data, error.error.message);
      }
    } else {
      // Client Error
      this.showError(error.message);
    }
  }
}
