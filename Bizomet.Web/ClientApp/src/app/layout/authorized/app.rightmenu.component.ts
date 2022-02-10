import { Component, OnInit } from '@angular/core';
import { ProfileService } from 'src/app/core/services/profile.service';
import { UserProfileModel } from 'src/app/shared/models/user-profile.model';
import { AuthorizedLayoutComponent } from './../authorized/authorized-layout/authorized-layout.component';

@Component({
    selector: 'app-rightmenu',
    templateUrl: './app.rightmenu.component.html'
})
export class AppRightMenuComponent implements OnInit {
    date: Date;
    currentProfile: UserProfileModel = new UserProfileModel();

    constructor(public appMain: AuthorizedLayoutComponent, private profileService: ProfileService) { }

    ngOnInit(): void {
        this.profileService.getProfile()
            .subscribe(data => this.currentProfile = data);
    }
}
