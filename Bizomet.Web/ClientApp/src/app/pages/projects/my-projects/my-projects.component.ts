import { Component, OnInit } from '@angular/core';
import { AppBreadcrumbService } from 'src/app/core/services/app.breadcrumb.service';
import { ConfirmationService, LazyLoadEvent, MessageService } from 'primeng/api';

import { ProjectModel } from 'src/app/shared/models/project.model';
import { ProjectService } from 'src/app/core/services/project.service';

@Component({
  selector: 'app-my-projects',
  templateUrl: './my-projects.component.html'
})
export class MyProjectsComponent implements OnInit {

  loading: boolean;
  projectList: ProjectModel[];
  project: ProjectModel;
  totalRecords: number = 0;

  constructor(
    private projectService: ProjectService, 
    private breadcrumbService: AppBreadcrumbService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService) {
      
    this.breadcrumbService.setItems([
      { label: 'Projects', icon: 'pi pi-fw pi-briefcase mr-1' },
      { label: 'My Projects' }
    ]);
  }

  ngOnInit(): void {
  }

  loadData(event: LazyLoadEvent) {
    this.loading = true;

    this.projectService.getAllByUser(event)
      .subscribe((res: any) => {
        this.projectList = res.data;
        this.totalRecords = res.total_records;
        this.loading = false;
      });
  }

  editProject(project: ProjectModel) {
  }

  deleteProject(project: ProjectModel) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete your project [' + project.title + ']? This action cannot be undone.',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.projectService.delete(project.id).subscribe(result => {
          this.projectList = this.projectList.filter(val => val.id !== project.id);
          this.project = new ProjectModel();
          this.totalRecords = this.totalRecords - 1;
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Project Deleted', life: 3000 });
        });
      }
    });
  }
}
