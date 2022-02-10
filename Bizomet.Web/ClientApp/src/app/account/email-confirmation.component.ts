import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Message } from 'primeng/api';
import { AuthenticationService } from 'src/app/core/services/authentication.service';

@Component({
  selector: 'app-email-confirmation',
  template: `
    <div class="layout-wrapper layout-horizontal">
      <div class="layout-main">
        <p-panel>
          <ng-template pTemplate="header">
            <h2 style="margin: 20px 30px;">Email Confirmation</h2>
          </ng-template>
          <p-messages [(value)]="message" [enableService]="false" [escape]="false" [closable]="false"></p-messages>
          <button *ngIf="successful" class="w-4 lg:w-2 py-3 my-3" type="button" pButton label="Go to login page" [routerLink]="['/account/login']"></button>
        </p-panel>
        <app-footer></app-footer>
      </div>
    </div>
  `
})
export class EmailConfirmationComponent implements OnInit {
  public message: Message[] = [];
  public successful: boolean = false;

  constructor(private _authenticationService: AuthenticationService, private _route: ActivatedRoute) { }

  ngOnInit(): void {
    const token = this._route.snapshot.queryParams['token'];
    const email = this._route.snapshot.queryParams['email'];

    this._authenticationService.confirmEmail(token, email)
      .subscribe(_ => {
        this.successful = true;
        this.message = [
          { severity: 'success', summary: '', detail: "Your email has been successfully confirmed" }
        ];
    },
        error => {
          this.message = [
            { severity: 'error', summary: '', detail: error }
          ];
        })
  }
}