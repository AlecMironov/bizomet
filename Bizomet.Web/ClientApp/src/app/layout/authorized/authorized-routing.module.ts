import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { InquiriesComponent } from 'src/app/pages/inquiries/inquiries.component';
import { ProfileComponent } from 'src/app/pages/profile/profile.component';
import { ProjectsComponent } from 'src/app/pages/projects/projects.component';

const routes: Routes = [
    { path: 'inquiries', component: InquiriesComponent, loadChildren: () => import('./../../pages/inquiries/inquires.module').then(m => m.InquiresModule)},
    { path: 'projects', component: ProjectsComponent, loadChildren: () => import('../../pages/projects/projects.module').then(m => m.ProjectsModule)},
    { path: 'profile', component: ProfileComponent, loadChildren: () => import('./../../pages/profile/profile.module').then(m => m.ProfileModule)}
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AuthorizedRoutingModule { }
