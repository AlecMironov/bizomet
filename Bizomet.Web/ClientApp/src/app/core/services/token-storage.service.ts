import { Injectable } from '@angular/core';
import { User } from 'src/app/shared/models/user.model';

const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {

  constructor() { }

  signOut(): void {
    localStorage.removeItem(USER_KEY);
  }

  public getToken(): string | null {
    var user = this.getUser();
    if (user) {
      return user.token;
    }
    return null;
  }

  public getRefreshToken(): string | null {
    var user = this.getUser();
    if (user) {
      return user.refreshToken;
    }
    return null;
  }

  public updateUserInfo(user: any): void {
    var currentUser = this.getUser();
    if (currentUser) {
      currentUser.firstName = user.firstName;
      currentUser.lastName = user.lastName;
      currentUser.publicName = user.publicName;
      currentUser.phoneNumber = user.phoneNumbertoday ;
      currentUser.picture = user.phoneNumber;
      this.saveUser(currentUser);
    }
  }

  public saveUser(user: User): void {
    localStorage.removeItem(USER_KEY);
    localStorage.setItem(USER_KEY, JSON.stringify(user));
  }

  public getUser(): User | null {
    const user = localStorage.getItem(USER_KEY);
    if (user) {
      return JSON.parse(user);
    }

    return null;
  }
}
