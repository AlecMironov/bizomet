import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddProjectComponent } from './add-project/add-project.component';
import { AllProjectsComponent } from './all-projects/all-projects.component';
import { MyProjectsComponent } from './my-projects/my-projects.component';

const routes: Routes = [
    { path: '', component: AllProjectsComponent },
    { path: 'my-projects', component: MyProjectsComponent },
    { path: 'add-project', component: AddProjectComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProjectsRoutingModule { }
