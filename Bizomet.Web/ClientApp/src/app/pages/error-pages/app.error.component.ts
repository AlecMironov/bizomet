import { Component } from '@angular/core';

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
    </div>
`
})
export class AppErrorComponent {
  constructor() { }
}
