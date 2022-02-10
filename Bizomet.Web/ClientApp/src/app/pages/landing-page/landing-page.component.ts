import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/core/services/authentication.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html'
})
export class LandingPageComponent implements OnInit {

  public showSignInButtons = false;
  private subscription;

  constructor(private authenticationService: AuthenticationService) { }

  viewBlocks(el: HTMLElement) {
    el.scrollIntoView({ behavior: "smooth" });
  }

  ngOnInit(): void {
    this.showSignInButtons = !this.authenticationService.isAuthenticated;
    this.subscription = this.authenticationService.onSignedIn$.subscribe((isSignedIn: boolean) => {
      this.showSignInButtons = !isSignedIn;
    });
  }

  public logout = () => {
    this.authenticationService.logout();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
