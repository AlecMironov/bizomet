import { Component, OnInit } from '@angular/core';
import { AppBreadcrumbService } from 'src/app/core/services/app.breadcrumb.service';
import { UserProfileModel } from 'src/app/shared/models/user-profile.model';
import { ProfileService } from 'src/app/core/services/profile.service';

@Component({
  selector: 'app-profile-overview',
  templateUrl: './profile-overview.component.html'
})
export class ProfileOverviewComponent implements OnInit {

  currentProfile: UserProfileModel = new UserProfileModel();

  constructor(private profileService: ProfileService, private breadcrumbService: AppBreadcrumbService) {
    this.breadcrumbService.setItems([
        { label: 'Profile', icon: 'pi pi-fw pi-user mr-1' },
        { label: 'My Profile' }
    ]);
  }

  ngOnInit(): void {
    this.profileService.getProfile()
      .subscribe(data => this.currentProfile = data);
  }
}
