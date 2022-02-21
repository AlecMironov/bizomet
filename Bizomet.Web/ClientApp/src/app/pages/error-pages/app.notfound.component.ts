import { Component } from '@angular/core';

@Component({
  selector: 'app-notfound',
  template: `
    <div class="exception-body notfound">
      <div class="exception-panel">
        <h1>404</h1>
        <h3>not found</h3>
        <p>The page that you are looking for does not exist</p>
        <button type="button" pButton label="Go back to home" [routerLink]="['/']"></button>
      </div>
    </div>
  `
})
export class AppNotfoundComponent {
    constructor() {}
}
