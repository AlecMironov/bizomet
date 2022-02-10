import { Component } from '@angular/core';
import { AppComponent } from '../../app.component';

@Component({
  selector: 'app-accessdenied',
  template: `
    <div class="exception-body accessdenied">
      <div class="exception-panel">
        <h1>ACCESS</h1>
        <h3>denied</h3>
        <p>You are not allowed to view this page.</p>
        <button type="button" pButton label="Go back to home" [routerLink]="['/']"></button>
      </div>
      <div class="exception-footer">
        <img [src]="'assets/layout/images/logo-'+ (app.colorScheme === 'light' ? 'dark' : 'light') + '.png'" class="exception-logo"/>
        <img [src]="'assets/layout/images/appname-'+ (app.colorScheme === 'light' ? 'dark' : 'light') + '.png'" class="exception-appname"/>
      </div>
    </div>
  `
})
export class AppAccessdeniedComponent {
  constructor(public app: AppComponent) { }
}
