import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService implements HttpInterceptor {

  constructor(private _router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
      .pipe(
        retry(1),
        catchError((error: HttpErrorResponse) => {
          let errorMessage = this.handleError(error);
          return throwError(errorMessage);
        })
      )
  }

  private handleError = (error: HttpErrorResponse): string | undefined => {
    if (error.status === 404) {
      return this.handleNotFound(error);
    }
    else if (error.status === 400) {
      return this.handleBadRequest(error);
    }
    else if (error.status === 401) {
      return this.handleUnauthorized(error);
    }
    else if (error.status === 423) {
      return this.handleLockedAccount(error);
    }
    else {
      return this.handle500Error(error);
    }
  }

  private handleLockedAccount = (error: HttpErrorResponse) => {
    if (this._router.url === '/account/login') {
      return error.error ? error.error.detail : error.message;
    }
    else {
      this._router.navigate(['/account/login']);
      return error.error ? error.error.detail : error.message;
    }
  }

  private handleUnauthorized = (error: HttpErrorResponse) => {
    if (this._router.url === '/account/login') {
      return error.error ? error.error : error.message;
    }
    else {
      this._router.navigate(['/account/login']);
      return error.error ? error.error : error.message;
    }
  }

  private handle500Error = (error: HttpErrorResponse): string => {
    this._router.navigate(['/error']);
    return error.error ? error.error : error.message;
  }

  private handleNotFound = (error: HttpErrorResponse): string => {
    this._router.navigate(['/pagenotfound']);
    return error.error ? error.error : error.message;
  }

  private handleBadRequest = (error: HttpErrorResponse): string => {
    if(this._router.url === '/account/register' || 
       this._router.url.startsWith('/account/resetpassword')) {
      let message = '';
      const values = Object.values(error.error.errors);
      values.map((m: string) => {
         message += m + '<br>';
      })

      return message.slice(0, -4);
    }
    else{
      return error.error ? error.error : error.message;
    }
  }
}
