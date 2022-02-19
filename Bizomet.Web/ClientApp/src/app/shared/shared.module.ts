import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

// Application Components
import { AppNotfoundComponent } from '../pages/error-pages/app.notfound.component';
import { AppAccessdeniedComponent } from '../pages/error-pages/app.accessdenied.component';
import { AppErrorComponent } from '../pages/error-pages/app.error.component';
import { PageContentComponent } from './../layout/page-content/page-content.component';
import { GuestLayoutComponent } from './../layout/guest/guest-layout/guest-layout.component';
import { LandingPageComponent } from './../pages/landing-page/landing-page.component';
import { AuthorizedLayoutComponent } from './../layout/authorized/authorized-layout/authorized-layout.component';
import { DashboardComponent } from './../pages/dashboard/dashboard.component';
import { AppBreadcrumbComponent } from './../layout/authorized/breadcrumb/app.breadcrumb.component';
import { AppMenuComponent } from './../layout/authorized/app.menu.component';
import { AppMenuitemComponent } from './../layout/authorized/app.menuitem.component';
import { AppConfigComponent } from './../layout/authorized/app.config.component';
import { AppRightMenuComponent } from './../layout/authorized/app.rightmenu.component';
import { AppInlineMenuComponent } from './../layout/authorized/app.inlinemenu.component';
import { AppTopbarComponent } from './../layout/authorized/app.topbar.component';
import { AppFooterComponent } from './../layout/authorized/app.footer.component';
import { PrimeNgModule } from '../primeng.module';

const sharedComponents = [
  AppErrorComponent, 
  AppNotfoundComponent, 
  AppAccessdeniedComponent,
  PageContentComponent,
  GuestLayoutComponent,
  LandingPageComponent,
  AuthorizedLayoutComponent,
  AppBreadcrumbComponent,
  AppMenuComponent,
  AppMenuitemComponent,
  AppConfigComponent,
  AppRightMenuComponent,
  AppInlineMenuComponent,
  AppTopbarComponent,
  AppFooterComponent,
  DashboardComponent
];

@NgModule({
  declarations: [...sharedComponents ],
  imports: [CommonModule, RouterModule, PrimeNgModule],
  exports: [...sharedComponents]
})
export class SharedModule {}
