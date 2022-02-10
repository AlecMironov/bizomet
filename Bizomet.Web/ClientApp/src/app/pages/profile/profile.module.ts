import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

// PrimeNg modules for account pages
import { BlockUIModule } from 'primeng/blockui';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { CarouselModule } from 'primeng/carousel';
import { CheckboxModule } from 'primeng/checkbox';
import { ChipModule } from 'primeng/chip';
import { DividerModule } from 'primeng/divider';
import { InputMaskModule } from 'primeng/inputmask';
import { InputTextModule } from 'primeng/inputtext';
import { MenuModule } from 'primeng/menu';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { PanelModule } from 'primeng/panel';
import { PasswordModule } from 'primeng/password';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { RippleModule } from 'primeng/ripple';

import { SharedModule } from '../../shared/shared.module';
import { ProfileRoutingModule } from './profile-routing.module';
import { ProfileComponent } from './profile.component';
import { ProfileAboutComponent } from './edit/about/about.component';
import { ProfileOverviewComponent } from './overview/profile-overview.component';
import { ProfilePortfolioComponent } from './edit/portfolio/portfolio.component';
import { MenubarModule } from 'primeng/menubar';
import { AvatarModule } from 'primeng/avatar';

@NgModule({
  declarations: [
    ProfileComponent,
    ProfileOverviewComponent,
    ProfileAboutComponent,
    ProfilePortfolioComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    AvatarModule,
    BlockUIModule,
    ButtonModule,
    CardModule,
    CarouselModule,
    CheckboxModule,
    ChipModule,
    DividerModule,
    InputMaskModule,
    InputTextModule,
    MessagesModule,
    MessageModule,
    MenuModule,
    MenubarModule,
    PanelModule,
    PasswordModule,
    ProgressSpinnerModule,
    RippleModule,
    ProfileRoutingModule,
    SharedModule
  ]
})
export class ProfileModule { }
