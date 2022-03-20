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
import { InquiresRoutingModule } from './inquires-routing.module';
import { SkeletonModule } from 'primeng/skeleton';
import { MegaMenuModule } from 'primeng/megamenu';
import { TabMenuModule } from 'primeng/tabmenu';
import { InquiriesComponent } from './inquiries.component';
import { AllInquiriesComponent } from './all-inquiries/all-inquiries.component';
import { MyInquiriesComponent } from './my-inquiries/my-inquiries.component';
import { MyPitchesComponent } from './my-pitches/my-pitches.component';

import { InquiryService } from 'src/app/core/services/inquiry.service';
import { AddInquiryComponent } from './add-inquiry/add-inquiry.component';

@NgModule({
  providers: [InquiryService, MessageService, ConfirmationService],
  declarations: [
    InquiriesComponent,
    AllInquiriesComponent,
    MyInquiriesComponent,
    MyPitchesComponent,
    AddInquiryComponent
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
    MegaMenuModule,
    MenuModule,
    MenubarModule,
    PasswordModule,
    ProgressSpinnerModule,
    RippleModule,
    SkeletonModule,
    ToastModule,
    TableModule,
    TabMenuModule,
    ToolbarModule,
    DialogModule,
    ImageCropperModule,
    InquiresRoutingModule,
    SharedModule
  ]
})
export class InquiresModule { }
