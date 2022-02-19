import { Component } from '@angular/core';

@Component({
  selector: 'app-under-construction',
  template: `
    <div class="layout-wrapper horizontal">
      <div class="layout-main">
        <h4>THIS PAGE IS UNDER CONSTRUCTION</h4>
        <h5>OUR TEAM IS WORKING HARD TO MAKE THIS PAGE UP!</h5>

        <div class="layout-footer flex flex-wrap">
          <img [src]="'assets/layout/images/logo-dark.png'"/>
        </div>
      </div>
    </div>
  `
})
export class UnderConstructionComponent {

  constructor() { }
}
