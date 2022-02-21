import { Component } from '@angular/core';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'app-terms-conditions',
  template: `
    <div class="exception-body underconstruction">
      <div class="exception-panel">
         <h3 class="text-500 mb-1">TERMS AND CONDITIONS</h3>
         <h1><small>THIS PAGE IS UNDER CONSTRUCTION</small></h1>
         <h3><small>OUR TEAM IS WORKING HARD TO MAKE THIS PAGE UP!</small></h3>
         <img src="assets/images/road-cones.png" style="max-width: 200px" alt="rode cones" class="w-full">
      </div>
    </div>
  `
})
export class TermsConditionsComponent {

  constructor(public app: AppComponent) { }
}
