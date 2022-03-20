import { Inject, Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject, map, Observable, Subject } from 'rxjs';
import { User } from '../../shared/models/user.model';
import { AuthResponseModel } from 'src/app/shared/models/auth-response.model';
import { UserAuthenticationModel } from 'src/app/shared/models/user-authentication.model';
import { TokenStorageService } from './token-storage.service';
import { UserRegistrationModel } from 'src/app/shared/models/user-registration.model';
import { TokenApiModel } from 'src/app/shared/models/token-api.model';
import { CustomEncoder } from 'src/app/shared/custom-encoder.module';
import { ForgotPasswordModel } from 'src/app/shared/models/forgot-password.model';
import { ResetPasswordModel } from 'src/app/shared/models/reset-password.model';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {

    private userSubject: BehaviorSubject<User>;
    public user: Observable<User>;

    private isSignedIn = new BehaviorSubject<boolean>(false);
    onSignedIn$ = this.isSignedIn.asObservable();

    constructor(
        private router: Router,
        private http: HttpClient,
        private tokenStorage: TokenStorageService,
        @Inject('BASE_URL') private baseUrl: string
    ) {
        this.userSubject = new BehaviorSubject<User>(tokenStorage.getUser());
        this.user = this.userSubject.asObservable();
        this.isSignedIn.next(false);
    }

    public get isAuthenticated(): boolean {
        const user = this.currentUser;
        this.isSignedIn.next(false); // <-- success

        if (!user || !user.token || !user.refreshToken) {
            return false;
        }

        const jwtHelper = new JwtHelperService();
        if (!jwtHelper.isTokenExpired(user.token)) {
            this.isSignedIn.next(true); // <-- success
            return true;
        }

        const isRefreshSuccess = this.tryRefreshingTokens(user);
        if (!isRefreshSuccess) {
            return false;
        }

        this.isSignedIn.next(true); // <-- success
        return true;
    }

    private async tryRefreshingTokens(user: User): Promise<boolean> {
        // Try refreshing tokens using refresh token
        if (!user || !user.token || !user.refreshToken) {
            return false;
        }
        const credentials = new TokenApiModel(user.token, user.refreshToken);
        let isRefreshSuccess: boolean;
        try {
            const response = await this.http.post<TokenApiModel>(this.createCompleteRoute("token/refresh"), credentials, {
                headers: new HttpHeaders({
                    "Content-Type": "application/json"
                }),
                observe: 'response'
            }).toPromise();
            // If token refresh is successful, set new tokens in local storage.
            user.token = response.body.accessToken;
            user.refreshToken = response.body.refreshToken;
            this.tokenStorage.saveUser(user);
            isRefreshSuccess = true;
        }
        catch (ex) {
            isRefreshSuccess = false;
        }
        return isRefreshSuccess;
    }

    public get currentUser(): User {
        return this.userSubject.value;
    }

    public login(model: UserAuthenticationModel) {
        this.tokenStorage.signOut();
        this.userSubject.next(null);
        this.isSignedIn.next(false); // <-- unsuccess

        return this.http.post<AuthResponseModel>(this.createCompleteRoute("account/login"), model)
            .pipe(map(response => {
                let user = new User();
                user.id = response.id;
                user.userName = response.userName;
                user.email = response.email;
                user.firstName = response.firstName;
                user.lastName = response.lastName;
                user.phoneNumber = response.phoneNumber;
                user.roles = response.roles;
                user.token = response.token;
                user.refreshToken = response.refreshToken;
                user.picture = this.getPictureUrl(response.picture);
                this.updateUserInfo(user);
                return user;
            }));
    }

    public logout() {
        if (this.isAuthenticated) {
            // remove user from local storage and set current user to null
            this.http.post(this.createCompleteRoute("account/logout"), null)
                .subscribe({
                    next: response => {
                        console.info('Successfuly logged out on the server');
                        this.tokenStorage.signOut();
                        this.userSubject.next(null);
                        this.isSignedIn.next(false); // <-- logged out
                        this.router.navigate(['/']);
                    },
                    error: error => {
                        console.error('Logout failed on the server: ', error);
                    }
                });
        }
    }

    public updateUserInfo(user: User): void {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        this.tokenStorage.saveUser(user)
        this.userSubject.next(user);
        this.isSignedIn.next(true); // <-- success
    }

    public confirmEmail = (token: string, email: string, username: string) => {
        let params = new HttpParams({ encoder: new CustomEncoder() })
        params = params.append('token', token);
        params = params.append('email', email);
        params = params.append('username', username);

        return this.http.get(this.createCompleteRoute("account/emailconfirmation"), { params: params });
    }

    public register(body: UserRegistrationModel) {
        return this.http.post(this.createCompleteRoute("account/register"), body);
    }

    public forgotPassword = (body: ForgotPasswordModel) => {
        return this.http.post(this.createCompleteRoute("account/forgotpassword"), body);
    }

    public resetPassword = (body: ResetPasswordModel) => {
        return this.http.post(this.createCompleteRoute("account/resetpassword"), body);
    }

    public validateUsername = (username: string) => {
        let params = new HttpParams({ encoder: new CustomEncoder() })
        params = params.append('username', username);

        return this.http.get(this.createCompleteRoute("account/validateusername"), { params: params });
    }

    public isUserAdmin = (): boolean => {
        return this.isUserInRole('Administrator');
    }

    public isUserTalent = (): boolean => {
        return this.isUserInRole('Talent');
    }

    public isUserLifter = (): boolean => {
        return this.isUserInRole('Lifter');
    }

    public isUserMediaAssistant = (): boolean => {
        return this.isUserInRole('MediaAssistant');
    }

    public isUserPromoter = (): boolean => {
        return this.isUserInRole('Promoter');
    }

    public isUserProducer = (): boolean => {
        return this.isUserInRole('Producer');
    }

    private isUserInRole = (role): boolean => {
        if (this.currentUser) {
            let _jwtHelper: JwtHelperService;
            const token = this.currentUser.token;
            const decodedToken = _jwtHelper.decodeToken(token);
            const roles = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
            return roles.includes(role);
        }
        else {
            return false;
        }
    }

    private createCompleteRoute = (route: string) => {
        return `${this.baseUrl}api/${route}`;
    }

    private getPictureUrl(data: string): string {
        if (typeof data != 'undefined' && data) {
            return `url(data:image/jpeg;base64,${data})`;
        }
        return null;
    }
}