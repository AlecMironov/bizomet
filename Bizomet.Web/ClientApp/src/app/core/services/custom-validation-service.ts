import { Injectable } from '@angular/core';
import { ValidatorFn, AbstractControl } from '@angular/forms';
import { AuthenticationService } from './authentication.service';

@Injectable({
    providedIn: 'root'
})
export class CustomvalidationService {

    constructor(
        private _authService: AuthenticationService) {
    }

    patternValidator(): ValidatorFn {
        return (control: AbstractControl): { [key: string]: any } => {
            if (!control.value) {
                return null;
            }
            const regex = new RegExp('^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$');
            const valid = regex.test(control.value);
            return valid ? null : { invalidPassword: true };
        };
    }

    validateConfirmPassword = (passwordControl: AbstractControl): ValidatorFn => {
        return (confirmationControl: AbstractControl): { [key: string]: boolean } | null => {

            const confirmValue = confirmationControl.value;
            const passwordValue = passwordControl.value;

            if (confirmValue === '') {
                return null;
            }
      
            if (confirmValue !== passwordValue) {
                return  { passwordMismatch: true }
            } 
      
            return null;
        };
    }

    userNameValidator(userControl: AbstractControl) {
        return new Promise(resolve => {
            setTimeout(() => {
                this._authService.validateUsername(userControl.value)
                    .subscribe((res) => {
                        resolve(null);
                    }, (err) => {
                        resolve({ userNameNotAvailable: true });
                    });
            }, 1000);
        });
    }
}