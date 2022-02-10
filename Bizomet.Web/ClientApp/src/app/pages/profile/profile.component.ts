import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { ProfileService } from 'src/app/core/services/profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html'
})
export class ProfileComponent implements OnInit {

  items: MenuItem[];

  constructor(private _router: Router) {
  }

  ngOnInit(): void {
    this.items = [{
      items: [{
        label: 'My Profile',
        command: () => {
          this._router.navigateByUrl('/profile');
        }
      }]
    },
    {
      label: 'PROFILE SETUP',
      items: [{
        label: 'About Me',
        command: () => {
          this._router.navigateByUrl('/profile/edit/about');
        }
      },
      {
        label: 'My Portfolio',
        command: () => {
          this._router.navigateByUrl('/profile/edit/portfolio');
        }
      }
      ]
    },
    {
      label: 'ACCOUNT SETUP',
      items: [{
        label: 'General',
        command: () => {
        }
      },
      {
        label: 'Notificatiions',
        command: () => {
        }
      }
      ]
    }
    ];
  }

  isDesktop() {
    return window.innerWidth > 991;
  }

  isMobile() {
    return window.innerWidth <= 991;
  }
}
