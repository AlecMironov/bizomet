import { Component } from '@angular/core';
import { AppComponent } from './../../app.component';

@Component({
    selector: 'app-footer',
    styles: [`
        .layout-footer {
            padding: 10px 14px;
            height: 5rem;
        }
        .layout-footer .copyright {
            display: inline-block;
            padding-bottom: 8px;
            margin-left: 4px;
        }
        .layout-footer .link-divider {
            color: #777777;
            margin: 0 10px;
        }
        .layout-footer .footer-links a {
            color: #777777;
            margin-left: 4px;
            margin-right: 4px;
            font-size: 0.9rem;
        }
        .layout-footer .footer-links a:hover {
            color: #494949;
        }
    `],
    template: `
        <div class="layout-footer flex flex-wrap">
            <!-- <div class="footer-logo-container pr-6">
                <a class="cursor-pointer" [routerLink]="['/']">
                    <img id="footer-logo" [src]="'../../assets/layout/images/logo-'+ (app.colorScheme === 'light' ? 'dark' : 'light') + '.png'" alt="bizomet logo"/>
                </a>
                <span class="app-name">BIZOMET</span>
            </div> -->
            <span class="copyright text-600 w-12">Copyright &#169; Bizomet, 2022</span>

            <div class="footer-links">
                <a routerLink="/aboutus">
                    <span class="hidden sm:inline">About Us</span>
                    <span class="inline sm:hidden">About</span>
                </a>

                <span class="link-divider">|</span>
                <a routerLink="/contactus">
                    <span class="hidden sm:inline">Contact Us</span>
                    <span class="inline sm:hidden">Contact</span>
                </a>

                <span class="link-divider">|</span>
                <a routerLink="/terms">
                    <span class="hidden sm:inline">Terms & Conditions</span>
                    <span class="inline sm:hidden">Terms</span>
                </a>

                <span class="link-divider">|</span>
                <a routerLink="/privacy">
                    <span class="hidden sm:inline">Privacy Policy</span>
                    <span class="inline sm:hidden">Privacy</span>
                </a>
            </div>
        </div>
    `
})
export class AppFooterComponent {
    constructor(public app: AppComponent) { }
}
