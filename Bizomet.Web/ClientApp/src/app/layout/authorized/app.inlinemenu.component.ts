import { Component, OnInit } from '@angular/core';
import { AuthorizedLayoutComponent } from './../authorized/authorized-layout/authorized-layout.component';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { AuthenticationService } from 'src/app/core/services/authentication.service';

@Component({
    selector: 'app-inlinemenu',
    templateUrl: './app.inlinemenu.component.html',
    animations: [
        trigger('inline', [
            state('hidden', style({
                height: '0px',
                overflow: 'hidden'
            })),
            state('visible', style({
                height: '*',
            })),
            state('hiddenAnimated', style({
                height: '0px',
                overflow: 'hidden'
            })),
            state('visibleAnimated', style({
                height: '*',
            })),
            transition('visibleAnimated => hiddenAnimated', animate('400ms cubic-bezier(0.86, 0, 0.07, 1)')),
            transition('hiddenAnimated => visibleAnimated', animate('400ms cubic-bezier(0.86, 0, 0.07, 1)'))
        ])
    ]
})
export class AppInlineMenuComponent implements OnInit {
    constructor(public appMain: AuthorizedLayoutComponent, public authService: AuthenticationService) { }

    ngOnInit(): void {
    }
}
