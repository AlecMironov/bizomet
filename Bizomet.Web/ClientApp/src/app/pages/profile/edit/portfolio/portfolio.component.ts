import { Component, OnInit } from '@angular/core';
import { AppBreadcrumbService } from 'src/app/core/services/app.breadcrumb.service';
import { UserProfileModel } from 'src/app/shared/models/user-profile.model';
import { ProfileService } from 'src/app/core/services/profile.service';

@Component({
  selector: 'app-portfolio',
  templateUrl: './portfolio.component.html'
})
export class ProfilePortfolioComponent implements OnInit {

  currentProfile: UserProfileModel = new UserProfileModel();

  constructor(private profileService: ProfileService, private breadcrumbService: AppBreadcrumbService) {
    this.breadcrumbService.setItems([
        { label: 'Profile',  routerLink: ['/profile'] },
        { label: 'Portfolio' }
    ]);
  }

  ngOnInit(): void {
    this.profileService.getProfile()
      .subscribe(data => this.currentProfile = data);
  }
}
