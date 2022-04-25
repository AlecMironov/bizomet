import { Component, OnInit, ViewChild } from '@angular/core';
import { AppBreadcrumbService } from 'src/app/core/services/app.breadcrumb.service';
import { ProjectService } from 'src/app/core/services/project.service';
import { ProjectModel } from 'src/app/shared/models/project.model';
import { LazyLoadEvent } from 'primeng/api';
import { SharedData } from 'src/app/shared/shared-data.module';
import { Table } from 'primeng/table'

@Component({
  selector: 'app-all-projects',
  templateUrl: './all-projects.component.html'
})
export class AllProjectsComponent implements OnInit {
  @ViewChild('dt') dt: Table | undefined;
  loading: boolean;
  projectList: ProjectModel[];
  totalRecords: number = 0;
  interview_results = SharedData.interview_results;
  interview_results_array = Array.from(this.interview_results, ([name, value]) => (value));
  interview_conditions = SharedData.interview_conditions;
  showProjectInfoPanel: boolean = false;
  selectedProject: ProjectModel = new ProjectModel();

  constructor(
    private projectService: ProjectService,
    private breadcrumbService: AppBreadcrumbService) {

    this.breadcrumbService.setItems([
      { label: 'Projects', icon: 'pi pi-fw pi-briefcase mr-1' },
      { label: 'All Projects' }
    ]);
  }

  ngOnInit(): void {
  }

  applyFilterGlobal($event, stringVal) {
    this.dt.filterGlobal(($event.target as HTMLInputElement).value, stringVal);
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

  onRowSelect(event) {
    this.showProjectInfoPanel = true;
  }

  onHideProjectInfoPanel(event) {
    this.selectedProject = new ProjectModel();
  }
}
