import { Component, OnInit } from '@angular/core';
import { AppBreadcrumbService } from 'src/app/core/services/app.breadcrumb.service';
import { UserProfileModel } from 'src/app/shared/models/user-profile.model';
import { ProfileService } from 'src/app/core/services/profile.service';
import { RepositoryService } from 'src/app/core/services/repository.service';
import { LookupModel } from 'src/app/shared/models/lookup.model';

@Component({
  selector: 'app-profile-overview',
  templateUrl: './profile-overview.component.html'
})
export class ProfileOverviewComponent implements OnInit {

  profile: UserProfileModel;
  contactReasons: LookupModel[];
  selectedReason: LookupModel;
  loading: boolean;

  constructor(private repositoryService: RepositoryService, private profileService: ProfileService, private breadcrumbService: AppBreadcrumbService) {
    this.breadcrumbService.setItems([
      { label: 'Profile', icon: 'pi pi-fw pi-user mr-1' },
      { label: 'My Profile' }
    ]);
  }

  ngOnInit(): void {
    this.loading = true;

    this.repositoryService.getAll<any>("common/contactusreason", null)
      .subscribe(res => {
        this.contactReasons = res;
        this.selectedReason = this.contactReasons[0];
      });

    this.profileService.getProfile()
      .subscribe(data => {
        this.profile = data;
        this.loading = false;
      });
  }
}
