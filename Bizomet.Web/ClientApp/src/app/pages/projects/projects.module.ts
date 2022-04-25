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
import { SkeletonModule } from 'primeng/skeleton';
import { MegaMenuModule } from 'primeng/megamenu';
import { TabMenuModule } from 'primeng/tabmenu';
import { ProjectsRoutingModule } from './projects-routing.module';
import { ProjectsComponent } from './projects.component';
import { AllProjectsComponent } from './all-projects/all-projects.component';
import { ProjectService } from '../../core/services/project.service';
import { MyProjectsComponent } from './my-projects/my-projects.component';
import { AddProjectComponent } from './add-project/add-project.component';
import { StepsModule } from 'primeng/steps';
import { TabViewModule } from 'primeng/tabview';
import { CalendarModule } from 'primeng/calendar';
import { InputSwitchModule } from 'primeng/inputswitch';
import { BadgeModule } from 'primeng/badge';
import { TimeAgoForwardPipe, } from 'src/app/core/services/time-ago-forward.pipe';
import { SidebarModule } from 'primeng/sidebar';

@NgModule({
  providers: [ProjectService, MessageService, ConfirmationService],
  declarations: [
    ProjectsComponent,
    AllProjectsComponent,
    MyProjectsComponent,
    AddProjectComponent,
    TimeAgoForwardPipe
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    AutoCompleteModule,
    FileUploadModule,
    BadgeModule,
    BlockUIModule,
    ButtonModule,
    CalendarModule,
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
    InputSwitchModule,
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
    SidebarModule,
    StepsModule,
    TabViewModule,
    ProjectsRoutingModule,
    SharedModule
  ]
})
export class ProjectsModule { }
