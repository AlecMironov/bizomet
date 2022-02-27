import { Component } from '@angular/core';
import { AppComponent } from 'src/app/app.component';
import { NavigationService } from 'src/app/core/services/navigation.service';

@Component({
  selector: 'app-support',
  template: `
    <div class="exception-body underconstruction">
      <div class="exception-panel">
         <h3 class="text-500 mb-1">SUPPORT</h3>
         <h1><small>THIS PAGE IS UNDER CONSTRUCTION</small></h1>
         <h3><small>OUR TEAM IS WORKING HARD TO MAKE THIS PAGE UP!</small></h3>
         <button class="mt-0 mb-6 p-button-secondary" icon="pi pi-arrow-left mr-3" iconPos="left" type="button" pButton label="Go back" (click)="navigateBack()"></button>
         <img src="assets/images/road-cones.png" style="max-width: 200px" alt="rode cones" class="w-full">
      </div>
    </div>
  `
})
export class SupportComponent {

  constructor(private navigation: NavigationService) { }

  navigateBack() {
    this.navigation.back();
  }
}
