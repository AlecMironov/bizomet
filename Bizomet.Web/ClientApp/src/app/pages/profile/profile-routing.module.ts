import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProfileAboutComponent } from 'src/app/pages/profile/edit/about/about.component';
import { ProfilePortfolioComponent } from 'src/app/pages/profile/edit/portfolio/portfolio.component';
import { ProfileOverviewComponent } from 'src/app/pages/profile/overview/profile-overview.component';

const routes: Routes = [
    { path: '', component: ProfileOverviewComponent },
    { path: 'edit/about', component: ProfileAboutComponent },
    { path: 'edit/portfolio', component: ProfilePortfolioComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProfileRoutingModule { }
