import { Component } from '@angular/core';

@Component({
  selector: 'app-under-construction',
  template: `
    <div class="exception-body underconstruction">
      <div class="exception-panel">
         <!-- <h3 class="text-200 mb-1">TERMS AND CONDITIONS</h3> -->
         <h1><small>THIS PAGE IS UNDER CONSTRUCTION</small></h1>
         <h3><small>OUR TEAM IS WORKING HARD TO MAKE THIS PAGE UP!</small></h3>
         <img src="assets/images/road-cones.png" style="max-width: 200px" alt="rode cones" class="w-full">
      </div>
    </div>
  `
})
export class UnderConstructionComponent {

  constructor() { }
}
