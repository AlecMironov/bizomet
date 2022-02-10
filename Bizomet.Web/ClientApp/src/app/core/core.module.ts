import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { AuthInterceptor } from './helpers/auth.interceptor';

// Application services
import { AppBreadcrumbService } from './services/app.breadcrumb.service';
import { MenuService } from './services/app.menu.service';
import { ErrorHandlerService } from './services/error-handler.service';

@NgModule({
  declarations: [],
  imports: [CommonModule],
  providers: [
    // { provide: LocationStrategy, useClass: PathLocationStrategy },
    // { provide: HTTP_INTERCEPTORS, useClass: AuthGuard, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorHandlerService, multi: true },
    MenuService, AppBreadcrumbService
  ]
})
export class CoreModule { }
