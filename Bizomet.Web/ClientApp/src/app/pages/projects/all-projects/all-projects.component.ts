import { Component, OnInit } from '@angular/core';
import { AppBreadcrumbService } from 'src/app/core/services/app.breadcrumb.service';
import { ProjectService } from 'src/app/core/services/project.service';
import { ProjectModel } from 'src/app/shared/models/project.model';
import { LazyLoadEvent } from 'primeng/api';

@Component({
  selector: 'app-all-projects',
  templateUrl: './all-projects.component.html'
})
export class AllProjectsComponent implements OnInit {

  loading: boolean;
  projectList: ProjectModel[];
  project: ProjectModel;
  totalRecords: number = 0;

  constructor(private projectService: ProjectService, private breadcrumbService: AppBreadcrumbService) {
    this.breadcrumbService.setItems([
      { label: 'Projects', icon: 'pi pi-fw pi-briefcase mr-1' },
      { label: 'All Projects' }
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
