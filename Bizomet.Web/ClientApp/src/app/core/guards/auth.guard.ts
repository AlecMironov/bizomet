import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({ providedIn: 'root' })

export class AuthGuard implements /* CanLoad, */ CanActivate {
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService) { }

    // async canLoad(route: Route, segments: UrlSegment[]) {
    //     if (this.authenticationService.isAuthenticated) {
    //         return true;
    //     } else {
    //         // not logged in so redirect to login page
    //         return this.router.createUrlTree(['/account/login', { queryParams: { returnUrl: segments.url }}]);
    //     }
    // }

    async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (this.authenticationService.isAuthenticated) {
            return true;
        } else {
            // not logged in so redirect to login page with the return url
            return this.router.createUrlTree(['/account/login'], { queryParams: { returnUrl: state.url } });
        }
    }
}