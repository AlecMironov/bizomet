import { LOCALE_ID, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

// PrimeNg modules for account pages
import { BlockUIModule } from 'primeng/blockui';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { CheckboxModule } from 'primeng/checkbox';
import { CarouselModule } from 'primeng/carousel';
import { DividerModule } from 'primeng/divider';
import { InputMaskModule } from 'primeng/inputmask';
import { InputTextModule } from 'primeng/inputtext';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { PanelModule } from 'primeng/panel';
import { PasswordModule } from 'primeng/password';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { RippleModule } from 'primeng/ripple';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { RegisterConfirmationComponent } from './register-confirmation.component';
import { ResetPasswordComponent } from './reset-password.component';

import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from '../shared/shared.module';
import { EmailConfirmationComponent } from './email-confirmation.component';
import { ForgotPasswordComponent } from './forgot-password.component';
import { MenuModule } from 'primeng/menu';
import { StepsModule } from 'primeng/steps';
import { TabViewModule } from 'primeng/tabview';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RecaptchaFormsModule, RecaptchaModule, RecaptchaSettings, RECAPTCHA_LANGUAGE, RECAPTCHA_SETTINGS } from 'ng-recaptcha';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    RegisterConfirmationComponent,
    ResetPasswordComponent,
    ForgotPasswordComponent,
    EmailConfirmationComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    BlockUIModule,
    ButtonModule,
    CardModule,
    CarouselModule,
    CheckboxModule,
    DividerModule,
    InputMaskModule,
    InputTextModule,
    MenuModule,
    MessagesModule,
    MessageModule,
    PanelModule,
    PasswordModule,
    ProgressSpinnerModule,
    RadioButtonModule,
    RippleModule,
    RecaptchaModule,
    RecaptchaFormsModule,
    StepsModule,
    TabViewModule,
    AccountRoutingModule,
    SharedModule
  ],
  providers: [
    {
      provide: RECAPTCHA_SETTINGS,
      useValue: {
        siteKey: environment.recaptcha.siteKey,
        theme: 'light',
      } as RecaptchaSettings
    },
    {
      provide: RECAPTCHA_LANGUAGE,
      useFactory: (locale: string) => locale,
      deps: [LOCALE_ID],
    }
  ]
})
export class AccountModule { }
