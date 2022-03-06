import { Component, OnInit } from '@angular/core';
import { UserProfileModel } from 'src/app/shared/models/user-profile.model';
import { ProfileService } from 'src/app/core/services/profile.service';
import { RepositoryService } from 'src/app/core/services/repository.service';
import { KeyValuePairModel } from 'src/app/shared/models/key-value-pair.model';
import { AppBreadcrumbService } from 'src/app/core/services/app.breadcrumb.service';

@Component({
  selector: 'app-all-projects',
  templateUrl: './all-projects.component.html'
})
export class AllProjectsComponent implements OnInit {

  profile: UserProfileModel;
  contactReasons: KeyValuePairModel[];
  selectedReason: KeyValuePairModel;
  loading: boolean;

  constructor(private repositoryService: RepositoryService, private profileService: ProfileService, private breadcrumbService: AppBreadcrumbService) {
    this.breadcrumbService.setItems([
      { label: 'Projects', icon: 'pi pi-fw pi-briefcase mr-1' },
      { label: 'All Projects' }
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
