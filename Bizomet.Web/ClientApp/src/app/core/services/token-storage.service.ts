import { Injectable } from '@angular/core';
import { User } from 'src/app/shared/models/user.model';

const USER_KEY = 'auth-user';
const USER_PROFILE_KEY = 'user-profile';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {

  constructor() { }

  signOut(): void {
    localStorage.removeItem(USER_KEY);
    localStorage.removeItem(USER_PROFILE_KEY);
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
