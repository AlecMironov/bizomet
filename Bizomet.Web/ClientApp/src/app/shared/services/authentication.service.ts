import { AuthResponseModel } from './../../shared/models/auth-response.model';
import { RegistrationResponseModel } from './../../shared/models/registration-response.model';
import { UserAuthenticationModel } from './../../shared/models/user-authentication.model';
import { UserRegistrationModel } from './../../shared/models/user-registration.model';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private _authChangeSub = new Subject<boolean>()
  public authChanged = this._authChangeSub.asObservable();
  private baseUrl: string;

  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string)
  {
    this.baseUrl = baseUrl;
  }

  public registerUser = (route: string, body: UserRegistrationModel) => {
    return this._http.post<RegistrationResponseModel>(this.createCompleteRoute(route, this.baseUrl), body);
  }

  public loginUser = (route: string, body: UserAuthenticationModel) => {
    return this._http.post<AuthResponseModel>(this.createCompleteRoute(route, this.baseUrl), body);
  }

  public logout = () => {
    localStorage.removeItem("token");
    this.sendAuthStateChangeNotification(false);
  }

  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this._authChangeSub.next(isAuthenticated);
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
