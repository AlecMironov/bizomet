import { Component } from '@angular/core';
import { AppComponent } from '../../app.component';

@Component({
  selector: 'app-error',
  template: `
    <div class="exception-body error">
      <div class="exception-panel">
        <h1>ERROR</h1>
        <h3>something's went
            wrong</h3>
        <button type="button" pButton label="Go back to home" [routerLink]="['/']"></button>
      </div>
      <div class="exception-footer">
        <img [src]="'assets/layout/images/logo-'+ (app.colorScheme === 'light' ? 'dark' : 'light') + '.png'" class="exception-logo"/>
        <img [src]="'assets/layout/images/appname-'+ (app.colorScheme === 'light' ? 'dark' : 'light') + '.png'" class="exception-appname"/>
      </div>
    </div>
`
})
export class AppErrorComponent {
  constructor(public app: AppComponent) { }
}
