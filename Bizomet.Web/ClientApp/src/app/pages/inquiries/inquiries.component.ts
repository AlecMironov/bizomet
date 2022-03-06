import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-inquiries',
  templateUrl: './inquiries.component.html'
})
export class InquiriesComponent implements OnInit {

  itemsBar: MenuItem[];

  constructor(private _router: Router) {
  }

  ngOnInit(): void {
    let allInquiriesMenuItem = {
      label: 'All Inquiries',
      command: () => {
        this._router.navigateByUrl('/inquiries');
      }
    };
    let myInquiriesMenuItem = {
      label: 'My Inquiries',
      command: () => {
        this._router.navigateByUrl('/inquiries/my-inquiries');
      }
    };
    let myResponsesMenuItem = {
      label: 'My Pitches',
      command: () => {
        this._router.navigateByUrl('/inquiries/my-pitches');
      }
    };

    this.itemsBar = [
      allInquiriesMenuItem,
      { label: '|', disabled: true, styleClass: "hidden lg:inline" },
      myInquiriesMenuItem,
      { label: '|', disabled: true, styleClass: "hidden lg:inline" },
      myResponsesMenuItem
    ];
  }
}