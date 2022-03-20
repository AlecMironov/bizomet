import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { MenuItemContent } from 'primeng/menu';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html'
})
export class ProjectsComponent implements OnInit {

  itemsBar: MenuItem[];

  constructor(private _router: Router) {
  }

  ngOnInit(): void {
    let allProjectsMenuItem = {
      label: 'All Projects',
      command: () => {
        this._router.navigateByUrl('/projects');
      }
    };
    let myProjectsMenuItem = {
      label: 'My Projects',
      command: () => {
        this._router.navigateByUrl('/projects/my-projects');
      }
    };

    this.itemsBar = [
      allProjectsMenuItem,
      { label: '|', disabled: true, styleClass: "hidden lg:inline" },
      myProjectsMenuItem
    ];
  }

  createNew() {
    const urlTree = this._router.createUrlTree(['/projects/add-project'], { queryParams: { returnUrl: this._router.url } });
    this._router.navigateByUrl(urlTree, { skipLocationChange: true });
  }
}