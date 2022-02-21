import { Component } from '@angular/core';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'app-contact-us',
  template: `
    <div class="exception-body underconstruction">
      <div class="exception-panel">
         <h3 class="text-500 mb-1">CONTACT US</h3>
         <h1><small>THIS PAGE IS UNDER CONSTRUCTION</small></h1>
         <h3><small>OUR TEAM IS WORKING HARD TO MAKE THIS PAGE UP!</small></h3>
         <img src="assets/images/road-cones.png" style="max-width: 200px" alt="rode cones" class="w-full">
      </div>
    </div>
  `
})
export class ContactUsComponent {

  constructor(public app: AppComponent) { }
}
