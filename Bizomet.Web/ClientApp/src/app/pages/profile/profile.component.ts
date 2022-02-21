import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { MenuItemContent } from 'primeng/menu';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html'
})
export class ProfileComponent implements OnInit {

  items: MenuItem[];
  itemsBar: MenuItem[];

  constructor(private _router: Router) {
  }

  ngOnInit(): void {
    let profileMenuItem = {
      label: 'My Profile',
      command: () => {
        this._router.navigateByUrl('/profile');
      }
    };
    let aboutMenuItem = {
      label: 'About Me',
      command: () => {
        this._router.navigateByUrl('/profile/edit/about');
      }
    };
    let portfolioMenuItem = {
      label: 'My Portfolio',
      command: () => {
        this._router.navigateByUrl('/profile/edit/portfolio');
      }
    };
    let generalMenuItem = {
      label: 'General',
      command: () => {
      }
    };
    let notificationMenuItem = {
      label: 'Notificatiions',
      command: () => {
      }
    };

    this.items = [
      { items: [profileMenuItem] },
      { label: 'PROFILE SETUP', items: [aboutMenuItem, portfolioMenuItem] },
      { label: 'ACCOUNT SETUP', items: [generalMenuItem, notificationMenuItem] }
    ];

    this.itemsBar = [
      profileMenuItem,
      { label: '|', disabled: true, styleClass: "hidden lg:inline" },
      aboutMenuItem,
      portfolioMenuItem, 
      { label: '|', disabled: true, styleClass: "hidden lg:inline" },
      generalMenuItem,
      notificationMenuItem
    ];
  }
}
