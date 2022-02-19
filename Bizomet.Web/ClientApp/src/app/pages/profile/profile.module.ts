import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ImageCropperModule } from 'ngx-image-cropper';

// PrimeNg modules for account pages
import { ConfirmationService, MessageService } from 'primeng/api';

import { BlockUIModule } from 'primeng/blockui';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { CarouselModule } from 'primeng/carousel';
import { CheckboxModule } from 'primeng/checkbox';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ChipModule } from 'primeng/chip';
import { ChipsModule } from 'primeng/chips';
import { DividerModule } from 'primeng/divider';
import { InputMaskModule } from 'primeng/inputmask';
import { InputTextModule } from 'primeng/inputtext';
import { MenubarModule } from 'primeng/menubar';
import { MenuModule } from 'primeng/menu';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { PanelModule } from 'primeng/panel';
import { PasswordModule } from 'primeng/password';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';
import { FileUploadModule } from 'primeng/fileupload';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';

import { SharedModule } from '../../shared/shared.module';
import { ProfileRoutingModule } from './profile-routing.module';
import { ProfileComponent } from './profile.component';
import { ProfileAboutComponent } from './edit/about/about.component';
import { ProfileOverviewComponent } from './overview/profile-overview.component';
import { ProfilePortfolioComponent } from './edit/portfolio/portfolio.component';

import { ProductService } from 'src/app/core/services/product-service';

@NgModule({
  providers: [ProductService, MessageService, ConfirmationService],
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
    AutoCompleteModule,
    FileUploadModule,
    BlockUIModule,
    ButtonModule,
    CardModule,
    CarouselModule,
    CheckboxModule,
    ChipModule,
    ChipsModule,
    ConfirmDialogModule,
    ConfirmPopupModule,
    DividerModule,
    DropdownModule,
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
    ToastModule,
    TableModule,
    ToolbarModule,
    DialogModule,
    ImageCropperModule,
    ProfileRoutingModule,
    SharedModule
  ]
})
export class ProfileModule { }
