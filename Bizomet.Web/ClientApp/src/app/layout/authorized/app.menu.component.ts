import { Component, OnInit } from '@angular/core';
import { AppComponent } from './../../app.component';
import { AuthorizedLayoutComponent } from './../authorized/authorized-layout/authorized-layout.component';

@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit {

    public model: any[];

    constructor(public app: AppComponent, public appMain: AuthorizedLayoutComponent) { }

    ngOnInit() {
        this.model = [
            {
                label: 'Dashboard', icon: 'pi pi-fw pi-desktop', routerLink: ['/dashboard'],
                // items: [
                //     { label: 'Overview', icon: 'pi pi-fw pi-desktop', routerLink: ['/dashboard'] }
                // ]
            },
            {
                label: 'Profile', icon: 'pi pi-fw pi-user', routerLink: ['/profile']
            }
            // {
            //     label: 'MenuItem2', icon: 'pi pi-fw pi-compass', routerLink: ['utilities']
            // },
            // {
            //     label: 'MenuItem3', icon: 'pi pi-fw pi-briefcase', routerLink: ['/pages']
            // }
        ];
    }
}
