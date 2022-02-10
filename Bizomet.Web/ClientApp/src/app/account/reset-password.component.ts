import { ActivatedRoute } from '@angular/router';
import { PasswordConfirmationValidatorService } from '../shared/custom-validators/password-confirmation-validator.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { ResetPasswordModel } from 'src/app/shared/models/reset-password.model';
import { Message, MessageService } from 'primeng/api';

@Component({
  selector: 'app-reset-password',
  template: `
    <p-blockUI [blocked]="disablePage">
      <p-progressSpinner></p-progressSpinner>
    </p-blockUI>
    <div class="layout-wrapper layout-horizontal">
      <div class="layout-main">
        <div class="flex flex-column align-items-center justify-content-center min-h-screen">
          <p-card class="w-10 md:w-7 lg:w-6 xl:w-4">
            <ng-template pTemplate="header">
              <h2 class="p-4">Reset Password</h2>
            </ng-template>
            
            <p-messages [(value)]="message" [enableService]="false" [closable]="false" [escape]="false"></p-messages>

            <form *ngIf="!completed" [formGroup]="resetPasswordForm" autocomplete="off" novalidate
              (ngSubmit)="resetPassword(resetPasswordForm.value)">

              <label for="password" class="block text-900 font-medium mb-2">New Password</label>
              <input p-password type="password" id="password" formControlName="password"
                class="w-full mb-3 p-3" autocomplete="off" pInputText placeholder="Password">
              <small class="p-error block mb-3"
                 *ngIf="validateControl('password') && hasError('password', 'required')">
                    Password is required
               </small>

               <label for="confirm" class="block text-900 font-medium mb-2">Confirm Password</label>
               <input p-password type="password" id="confirm" formControlName="confirm" class="w-full mb-3 p-3"
                autocomplete="off" pInputText placeholder="Confirm Password">
               <small class="p-error block mb-3"
                  *ngIf="validateControl('confirm') && hasError('confirm', 'required')">
                     Confirmation is required
                </small>
                <small class="p-error block mb-3" *ngIf="hasError('confirm', 'mustMatch')">Passwords must match</small>

                <button pButton pRipple label="Reset password" type="submit" [disabled]="!resetPasswordForm.valid"
                  class="w-full py-3 my-4 font-medium"></button>
            </form>
            <ng-template pTemplate="footer">
              <button *ngIf="completed" class="w-full py-3 my-4 font-medium" type="button" pButton label="Go to Login page" [routerLink]="['account/login']"></button>
            </ng-template>
          </p-card>
          <app-footer></app-footer>
        </div>
      </div>
    </div>
  `,
  providers: [MessageService]
})
export class ResetPasswordComponent implements OnInit {
  public resetPasswordForm: FormGroup;
  public message: Message[] = [];
  public completed: boolean = false;
  public disablePage: boolean = false;

  private _token: string;
  private _email: string;

  constructor(
    private _authenticationService: AuthenticationService,
    private _passConfValidator: PasswordConfirmationValidatorService,
    private _route: ActivatedRoute) { }

  ngOnInit(): void {
    this.resetPasswordForm = new FormGroup({
      password: new FormControl('', [Validators.required]),
      confirm: new FormControl('')
    });
    this.resetPasswordForm.get('confirm').setValidators([Validators.required,
    this._passConfValidator.validateConfirmPassword(this.resetPasswordForm.get('password'))]);

    this._token = this._route.snapshot.queryParams['token'];
    this._email = this._route.snapshot.queryParams['email'];
  }

  public validateControl = (controlName: string) => {
    return this.resetPasswordForm.controls[controlName].invalid && this.resetPasswordForm.controls[controlName].touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.resetPasswordForm.controls[controlName].hasError(errorName)
  }

  public resetPassword = (resetPasswordFormValue) => {
    this.message = [];
    this.disablePage = true;

    const resetPass = { ...resetPasswordFormValue };
    const model: ResetPasswordModel = {
      password: resetPass.password,
      confirmPassword: resetPass.confirm,
      token: this._token,
      email: this._email
    }

    this._authenticationService.resetPassword(model)
      .subscribe(_ => {
        this.completed = true;
        this.disablePage = false;
        this.message = [
          { severity: 'success', summary: '', detail: 'New password has been set.' }
        ];
      },
        (error) => {
          this.disablePage = false;
          this.message = [
            { severity: 'error', summary: '', detail: error }
          ];
        })
  }
}