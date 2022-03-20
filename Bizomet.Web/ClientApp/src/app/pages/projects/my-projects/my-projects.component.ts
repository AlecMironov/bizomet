import { Component, OnInit } from '@angular/core';
import { AppBreadcrumbService } from 'src/app/core/services/app.breadcrumb.service';
import { ProjectModel } from 'src/app/shared/models/project.model';
import { ProjectService } from 'src/app/core/services/project.service';
import { LazyLoadEvent } from 'primeng/api';

@Component({
  selector: 'app-my-projects',
  templateUrl: './my-projects.component.html'
})
export class MyProjectsComponent implements OnInit {

  loading: boolean;
  projectList: ProjectModel[];
  project: ProjectModel;
  totalRecords: number = 0;

  constructor(private projectService: ProjectService, private breadcrumbService: AppBreadcrumbService) {
    this.breadcrumbService.setItems([
      { label: 'Projects', icon: 'pi pi-fw pi-briefcase mr-1' },
      { label: 'My Projects' }
    ]);
  }

  ngOnInit(): void {
  }

  loadData(event: LazyLoadEvent) {
    this.loading = true;

    this.projectService.getAll(event)
      .subscribe((res: any) => {
        this.projectList = res.data;
        this.totalRecords = res.total_records;
        this.loading = false;
      });
  }
}
