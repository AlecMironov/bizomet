import { Component } from '@angular/core';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'app-privacy',
  template: `
    <div class="exception-body accessdenied">
      <div class="exception-panel">
        <h3>PRIVACY POLICY</h3>
      </div>
      <div class="exception-footer">
        <img [src]="'assets/layout/images/logo-'+ (app.colorScheme === 'light' ? 'dark' : 'light') + '.png'" class="exception-logo"/>
        <img [src]="'assets/layout/images/appname-'+ (app.colorScheme === 'light' ? 'dark' : 'light') + '.png'" class="exception-appname"/>
      </div>
    </div>
  `
})
export class PrivacyComponent {

  constructor(public app: AppComponent) { }
}
