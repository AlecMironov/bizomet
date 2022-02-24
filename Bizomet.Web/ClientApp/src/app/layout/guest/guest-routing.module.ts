import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminGuard } from 'src/app/core/guards/admin.guard';
import { AboutUsComponent } from 'src/app/pages/common/about-us.component';
import { ContactUsComponent } from 'src/app/pages/common/contact-us.component';
import { CookiePolicyComponent } from 'src/app/pages/common/cookiepolicy.component';
import { PrivacyComponent } from 'src/app/pages/common/privacy.component';
import { SupportComponent } from 'src/app/pages/common/support.component';
import { TermsConditionsComponent } from 'src/app/pages/common/terms-conditions.component';
import { AppAccessdeniedComponent } from 'src/app/pages/error-pages/app.accessdenied.component';
import { AppErrorComponent } from 'src/app/pages/error-pages/app.error.component';
import { AppNotfoundComponent } from 'src/app/pages/error-pages/app.notfound.component';

import { PlaygroundComponent } from 'src/app/pages/playground/playground';

const routes: Routes = [
    { path: 'playground', component: PlaygroundComponent, pathMatch: 'full', canActivate: [AdminGuard] },
    { path: 'aboutus', pathMatch: 'full', component: AboutUsComponent },
    { path: 'contactus', pathMatch: 'full', component: ContactUsComponent },
    { path: 'terms', pathMatch: 'full', component: TermsConditionsComponent },
    { path: 'privacy', pathMatch: 'full', component: PrivacyComponent },
    { path: 'cookiepolicy', pathMatch: 'full', component: CookiePolicyComponent },
    { path: 'support', pathMatch: 'full', component: SupportComponent },
    { path: 'accessdenied', pathMatch: 'full', component: AppAccessdeniedComponent },
    { path: 'error', pathMatch: 'full', component: AppErrorComponent },
    { path: '**', pathMatch: 'full', component: AppNotfoundComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class GuestRoutingModule { }
