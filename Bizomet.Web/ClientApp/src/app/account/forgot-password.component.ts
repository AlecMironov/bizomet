import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Message, MessageService } from 'primeng/api';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { ForgotPasswordModel } from 'src/app/shared/models/forgot-password.model';

@Component({
  selector: 'app-forgot-password',
  template: `
    <p-blockUI [blocked]="disablePage">
      <p-progressSpinner></p-progressSpinner>
    </p-blockUI>
    <div class="layout-wrapper layout-horizontal">
      <div class="layout-main">
        <div class="flex flex-column align-items-center justify-content-center min-h-screen">
          <p-card class="w-10 md:w-7 lg:w-6 xl:w-4">
            <ng-template pTemplate="title">
              <h2 class="px-4 pt-4">Forgot your password?</h2>
            </ng-template>

            <ng-template pTemplate="subtitle">
              <h6 class="px-4">Enter your email</h6>
            </ng-template>

            <p-messages [(value)]="message" [enableService]="false" [closable]="false" [escape]="false"></p-messages>

            <form *ngIf="!completed" [formGroup]="forgotPasswordForm" autocomplete="off" novalidate
              (ngSubmit)="forgotPassword(forgotPasswordForm.value)">

              <label for="email" class="block text-900 font-medium mb-2">Email</label>
              <input id="email" formControlName="email" type="text" pInputText class="w-full mb-3 p-3"
                autocomplete="on" pInputText placeholder="Your email">
              <small class="p-error block mb-3" *ngIf="validateControl('email') && hasError('email', 'required')">Email is required</small>

              <button pButton pRipple label="Send Link" type="submit" [disabled]="!forgotPasswordForm.valid"
                class="w-full py-3 font-medium"></button>
            </form>
            <ng-template pTemplate="footer">
              <button *ngIf="completed" class="w-full py-3" type="button" pButton label="Return to home page" [routerLink]="['/']"></button>
            </ng-template>
          </p-card>
          <app-footer></app-footer>
        </div>
      </div>
    </div>
  `,
  providers: [MessageService]
})
export class ForgotPasswordComponent implements OnInit {
  public forgotPasswordForm: FormGroup
  public message: Message[] = [];
  public completed: boolean = false;
  public disablePage: boolean = false;

  constructor(private _authenticationService: AuthenticationService, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit(): void {
    this.forgotPasswordForm = new FormGroup({
      email: new FormControl("", [Validators.required])
    })
  }

  public validateControl = (controlName: string) => {
    return this.forgotPasswordForm.controls[controlName].invalid && this.forgotPasswordForm.controls[controlName].touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.forgotPasswordForm.controls[controlName].hasError(errorName)
  }

  public forgotPassword = (forgotPasswordFormValue) => {
    this.message = [];
    this.disablePage = true;

    const forgotPass = { ...forgotPasswordFormValue };
    const model: ForgotPasswordModel = {
      email: forgotPass.email,
      clientURI: `${this.baseUrl}account/resetpassword`
    }

    this._authenticationService.forgotPassword(model)
      .subscribe(_ => {
        this.completed = true;
        this.disablePage = false;
        this.message = [
          { severity: 'success', summary: '', detail: 'The link has been sent, please check your email to reset your password.' }
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