import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { GuestLayoutComponent } from './layout/guest/guest-layout/guest-layout.component';
import { AuthorizedLayoutComponent } from './layout/authorized/authorized-layout/authorized-layout.component';
import { LandingPageComponent } from './pages/landing-page/landing-page.component';
import { AppNotfoundComponent } from './pages/error-pages/app.notfound.component';
import { AuthGuard } from './core/guards/auth.guard';
import { PlaygroundComponent } from './pages/playground/playground';
import { AppAccessdeniedComponent } from './pages/error-pages/app.accessdenied.component';
import { AppErrorComponent } from './pages/error-pages/app.error.component';
import { AdminGuard } from './core/guards/admin.guard';

const routes: Routes = [
    {
        path: '',
        component: GuestLayoutComponent,
        children: [
            { path: '', component: LandingPageComponent, pathMatch: 'full' },
            { path: 'playground', component: PlaygroundComponent, pathMatch: 'full' },
        ]
    },
    {
        path: '',
        component: AuthorizedLayoutComponent,
        canActivate: [AuthGuard],
        loadChildren: () => import('./layout/authorized/authorized.module').then(m => m.AuthorizedModule)
    },
    { path: 'account', loadChildren: () => import('./account/account.module').then(m => m.AccountModule), data: {name: 'Account'} },
    { path: 'accessdenied', pathMatch: 'full', component: AppAccessdeniedComponent },
    { path: 'error', pathMatch: 'full', component: AppErrorComponent },
    { path: '**', pathMatch: 'full', component: AppNotfoundComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' })],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
