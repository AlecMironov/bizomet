import { Component, OnDestroy } from '@angular/core';
import { AppBreadcrumbService } from './../../../core/services/app.breadcrumb.service';
import { Subscription } from 'rxjs';
import { MenuItem } from 'primeng/api';
import { AuthorizedLayoutComponent } from './../../authorized/authorized-layout/authorized-layout.component';

@Component({
    selector: 'app-breadcrumb',
    templateUrl: './app.breadcrumb.component.html'
})
export class AppBreadcrumbComponent implements OnDestroy {

    subscription: Subscription;

    items: MenuItem[];

    home: MenuItem;

    search: string;

    constructor(public breadcrumbService: AppBreadcrumbService, public appMain: AuthorizedLayoutComponent) {
        this.subscription = breadcrumbService.itemsHandler.subscribe(response => {
            this.items = response;
        });

        this.home = { icon: 'pi pi-home', routerLink: '/' };
    }

    ngOnDestroy() {
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    }
}
