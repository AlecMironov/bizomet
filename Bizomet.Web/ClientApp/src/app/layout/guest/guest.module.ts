import { LOCALE_ID, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

// PrimeNg modules for authorized pages
import { BlockUIModule } from 'primeng/blockui';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { CarouselModule } from 'primeng/carousel';
import { DividerModule } from 'primeng/divider';
import { DropdownModule } from 'primeng/dropdown';
import { InputMaskModule } from 'primeng/inputmask';
import { InputTextModule } from 'primeng/inputtext';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { PanelModule } from 'primeng/panel';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { RippleModule } from 'primeng/ripple';
import { CardModule } from 'primeng/card';

import { SharedModule } from '../../shared/shared.module';
import { GuestRoutingModule } from './guest-routing.module';
import { AboutUsComponent } from 'src/app/pages/common/about-us.component';
import { ContactUsComponent } from 'src/app/pages/common/contact-us.component';
import { CookiePolicyComponent } from 'src/app/pages/common/cookiepolicy.component';
import { PrivacyComponent } from 'src/app/pages/common/privacy.component';
import { SupportComponent } from 'src/app/pages/common/support.component';
import { TermsConditionsComponent } from 'src/app/pages/common/terms-conditions.component';
import { UnderConstructionComponent } from 'src/app/pages/common/under-construction.component';
import { RecaptchaFormsModule, RecaptchaModule, RecaptchaSettings, RECAPTCHA_LANGUAGE, RECAPTCHA_SETTINGS } from 'ng-recaptcha';
import { environment } from 'src/environments/environment';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@NgModule({
  declarations: [
    AboutUsComponent,
    ContactUsComponent,
    CookiePolicyComponent,
    PrivacyComponent,
    SupportComponent,
    TermsConditionsComponent,
    UnderConstructionComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    BlockUIModule,
    ButtonModule,
    CardModule,
    CheckboxModule,
    DividerModule,
    DropdownModule,
    InputMaskModule,
    InputTextModule,
    ProgressSpinnerModule,
    RippleModule,
    MessagesModule,
    MessageModule,
    ToastModule,
    RecaptchaModule,
    RecaptchaFormsModule,
    GuestRoutingModule,
    SharedModule
  ],
  providers: [
    MessageService,
    {
      provide: RECAPTCHA_SETTINGS,
      useValue: {
        siteKey: environment.recaptcha.siteKey,
        theme: 'light',
      } as RecaptchaSettings
    },
    // {
    //   provide: RECAPTCHA_LANGUAGE,
    //   useValue: "ru", // use Russian language
    // },
    {
      provide: RECAPTCHA_LANGUAGE,
      useFactory: (locale: string) => locale,
      deps: [LOCALE_ID],
    }
  ]
})
export class GuestModule { }
