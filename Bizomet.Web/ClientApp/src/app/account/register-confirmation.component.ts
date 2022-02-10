import { Component } from '@angular/core';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-register-confirmation',
  template: `
    <div class="layout-wrapper layout-horizontal">
      <div class="layout-main">
        <div class="flex flex-column align-items-center justify-content-center min-h-screen">
          <p-card class="w-10 md:w-7 lg:w-6 xl:w-4">
            <ng-template pTemplate="header">
              <h2 class="p-4">Confirmation Email sent</h2>
            </ng-template>

          <p-message severity="success" text="Please check your email and confirm your email address" [escape]="false"></p-message>  

          <ng-template pTemplate="footer">
            <button class="w-full py-3" type="button" pButton label="Return to home page" [routerLink]="['/']"></button>
          </ng-template>

          </p-card>
          <app-footer></app-footer>
        </div>
      </div>
    </div>
  `,
  providers: [MessageService]
})
export class RegisterConfirmationComponent {
  constructor() { }
}
