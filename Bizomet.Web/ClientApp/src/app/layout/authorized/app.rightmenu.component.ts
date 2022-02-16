import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { AuthorizedLayoutComponent } from './../authorized/authorized-layout/authorized-layout.component';

@Component({
    selector: 'app-rightmenu',
    templateUrl: './app.rightmenu.component.html'
})
export class AppRightMenuComponent implements OnInit {
    constructor(public appMain: AuthorizedLayoutComponent, public authService: AuthenticationService) { }

    ngOnInit(): void {
    }
}
