import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AllProjectsComponent } from './all-projects/all-projects.component';
import { MyProjectsComponent } from './my-projects/my-projects.component';

const routes: Routes = [
    { path: '', component: AllProjectsComponent },
    { path: 'my-projects', component: MyProjectsComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProjectsRoutingModule { }
