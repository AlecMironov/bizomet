import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { GuestLayoutComponent } from './layout/guest/guest-layout/guest-layout.component';
import { AuthorizedLayoutComponent } from './layout/authorized/authorized-layout/authorized-layout.component';
import { LandingPageComponent } from './pages/landing-page/landing-page.component';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
    {
        path: '', component: LandingPageComponent, pathMatch: 'full'
    },
    {
        path: '',
        component: AuthorizedLayoutComponent,
        canActivate: [AuthGuard],
        loadChildren: () => import('./layout/authorized/authorized.module').then(m => m.AuthorizedModule)
    },
    { path: 'account', loadChildren: () => import('./account/account.module').then(m => m.AccountModule) },
    {
        path: '',
        component: GuestLayoutComponent,
        loadChildren: () => import('./layout/guest/guest.module').then(m => m.GuestModule)
        // children: [
        // ]
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' })],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
