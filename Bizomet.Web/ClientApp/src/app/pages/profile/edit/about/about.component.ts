import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Message } from 'primeng/api/message';
import { Dimensions, ImageCroppedEvent, ImageTransform } from 'ngx-image-cropper';
import { AppBreadcrumbService } from 'src/app/core/services/app.breadcrumb.service';
import { ProfileService } from 'src/app/core/services/profile.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { UserRole } from 'src/app/shared/models/user-role.model';
import { SharedData } from 'src/app/shared/shared-data.module';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styles: [`
  .loader {
      position: absolute;
      top: 0;
      bottom: 0;
      left: 0;
      right: 0;
      background-color: rgba(0, 0, 0, .5);
      display: flex;
      justify-content: center;
      align-items: center;
      font-size: 1.2rem;
      color: white;
    }  
  `]
})
export class ProfileAboutComponent implements OnInit {

  aboutMeForm: FormGroup;
  errorMessage: Message[] = [];
  disablePage = false;
  uploadedFiles: any[] = [];
  imageChangedEvent: any = '';
  imageBase64String: string = '';
  croppedImage: any = '';
  displayCropDialog: boolean = false;
  scale = 1;
  translateH = 0;
  translateV = 0;
  transform: ImageTransform = {};
  loading = false;
  showCropper = false;
  originalPicture: string;
  currentPicture: string;
  refreshRequired: boolean = false;
  roleList: UserRole[];
  //tagList: string[] = [];

  constructor(
    private profileService: ProfileService,
    private authService: AuthenticationService,
    private breadcrumbService: AppBreadcrumbService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService) {

    this.breadcrumbService.setItems([
      { label: 'Profile', routerLink: ['/profile'] },
      { label: 'About Me' }
    ]);

    this.aboutMeForm = new FormGroup({
      firstName: new FormControl("", [Validators.required]),
      lastName: new FormControl("", [Validators.required]),
      description: new FormControl(""),
      roles: new FormControl("", [Validators.required]),
      tags: new FormControl("")
    });
  }

  ngOnInit(): void {
    this.errorMessage = [];
    this.uploadedFiles = [];
    this.imageChangedEvent = '';
    this.imageBase64String = '';
    this.croppedImage = '';
    this.displayCropDialog = false;
    this.scale = 1;
    this.translateH = 0;
    this.translateV = 0;
    this.transform = {};
    this.loading = false;
    this.showCropper = false;

    this.disablePage = true;
    this.profileService.getProfile()
      .subscribe(data => {
        this.aboutMeForm.controls["firstName"].setValue(data.firstName);
        this.aboutMeForm.controls["lastName"].setValue(data.lastName);
        this.aboutMeForm.controls["description"].setValue(data.description);
        this.aboutMeForm.controls["roles"].setValue(SharedData.all_roles.filter(x => data.roles.includes(x.key)));
        this.aboutMeForm.controls["tags"].setValue(data.tags);
        this.originalPicture = data.picture;
        this.currentPicture = data.picture;
        //this.tagList.push(...data.tags);

        if (this.refreshRequired) {
          let user = this.authService.currentUser;
          user.firstName = data.firstName;
          user.lastName = data.lastName;
          user.picture = data.picture;
          user.roles = data.roles;
          this.authService.updateUserInfo(user);
          this.refreshRequired = false;
        }
        this.disablePage = false;
      });
  }

  public validateControl = (controlName: string) => {
    return this.aboutMeForm.controls[controlName].invalid && this.aboutMeForm.controls[controlName].touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.aboutMeForm.controls[controlName].hasError(errorName)
  }

  getRoles(event) {
    this.roleList = SharedData.all_roles.filter(r => r.name.toLowerCase().startsWith(event.query.toLowerCase()));
  }

  // getTags(event) {
  //   this.tagList = this.tagList.filter(r => r.toLocaleLowerCase().includes(event.query.toLowerCase()));
  // }

  // onTagKeyUp(event: KeyboardEvent) {
  //   if (event.key == "Enter") {
  //     let tokenInput = event.srcElement as any;
  //     if (tokenInput.value) {
  //       this.tagList.push(tokenInput.value);
  //       tokenInput.value = "";
  //     }
  //   }
  // }

  public saveAction = (formValue: any) => {
    this.disablePage = true;
    this.errorMessage = [];
    var dirtyValues = this.getDirtyValues(this.aboutMeForm);

    if (this.currentPicture != this.originalPicture) {
      const pic = typeof this.currentPicture != 'undefined' && this.currentPicture ? this.currentPicture.substring(26, this.currentPicture.length - 1) : "";
      dirtyValues["picture"] = pic;
    }

    if (Object.prototype.hasOwnProperty.call(dirtyValues, 'roles')) {
      let originalValue = dirtyValues["roles"];
      dirtyValues["roles"] = originalValue.map((r: UserRole) => r.key);
    }

    if (JSON.stringify(dirtyValues) != '{}') {
      this.profileService.updateProfile(dirtyValues)
        .subscribe(() => {
          this.refreshRequired = true;
          this.ngOnInit();
          this.messageService.add({ severity: 'success', summary: 'Saved', detail: '' });
        });
    } else {
      this.disablePage = false;
    }
  }

  getDirtyValues(form: any) {
    let dirtyValues = {};

    Object.keys(form.controls)
      .forEach(key => {
        let currentControl = form.controls[key];

        if (currentControl.dirty) {
          if (currentControl.controls)
            dirtyValues[key] = this.getDirtyValues(currentControl);
          else
            dirtyValues[key] = currentControl.value;
        }
      });

    return dirtyValues;
  }

  public confirmCancel(event: Event) {
    this.confirmationService.confirm({
      target: event.target,
      message: 'You will loose all your changes. Click Yes to proceed.',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.cancelAction();
      },
      reject: () => {
      }
    });
  }

  public cancelAction = () => {
    this.disablePage = true;
    setTimeout(() => {
      this.ngOnInit();
    }, 150)
  }

  //isImageSaved = false;
  //imageError: string;

  // fileChangeEvent2(fileInput: any) {
  //   this.imageError = null;
  //   if (fileInput.target.files && fileInput.target.files[0]) {
  //     // Size Filter Bytes
  //     const max_size = 20971520;
  //     const allowed_types = ['image/png', 'image/jpeg'];
  //     const max_height = 15200;
  //     const max_width = 25600;

  //     if (fileInput.target.files[0].size > max_size) {
  //       this.imageError =
  //         'Maximum size allowed is ' + max_size / 1000 + 'Mb';

  //       return false;
  //     }

  //     if (!_.includes(allowed_types, fileInput.target.files[0].type)) {
  //       this.imageError = 'Only Images are allowed ( JPG | PNG )';
  //       return false;
  //     }
  //     const reader = new FileReader();
  //     reader.onload = (e: any) => {
  //       const image = new Image();
  //       image.src = e.target.result;
  //       image.onload = rs => {
  //         const img_height = rs.currentTarget['height'];
  //         const img_width = rs.currentTarget['width'];
  //         console.log(img_height, img_width);
  //         if (img_height > max_height && img_width > max_width) {
  //           this.imageError =
  //             'Maximum dimentions allowed ' +
  //             max_height +
  //             '*' +
  //             max_width +
  //             'px';
  //           return false;
  //         } else {
  //           const imgBase64Path = e.target.result;

  //           this.currentProfile.pictureLarge = imgBase64Path;
  //           this.isImageSaved = true;

  //           return true;
  //         }
  //       };
  //     };

  //     reader.readAsDataURL(fileInput.target.files[0]);
  //   }
  //   return false;
  // }

  removeImage() {
    this.currentPicture = null;
  }

  editImage() {
    if (typeof this.currentPicture != 'undefined' && this.currentPicture) {
      this.imageBase64String = this.currentPicture.substring(4, this.currentPicture.length - 1);
      this.loading = true;
      this.scale = 1;
      this.displayCropDialog = true;
    }
  }

  fileChangeEvent(event: any): void {
    this.imageBase64String = null;
    if (event.target.files && event.target.files[0]) {
      this.loading = true;
      this.scale = 1;
      this.imageChangedEvent = event;
      this.displayCropDialog = true;
    }
  }

  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = event.base64;
  }

  imageLoaded() {
    this.showCropper = true;
  }

  cropperReady(sourceImageDimensions: Dimensions) {
    this.loading = false;
  }

  zoomOut() {
    if (this.scale >= 1) {
      this.scale -= .5;
      this.transform = {
        ...this.transform,
        scale: this.scale
      };
    }
  }

  zoomOutIsInRange() {
    return this.scale >= 1 && this.scale <= 6
  }

  zoomIn() {
    if (this.scale <= 6) {
      this.scale += .5;
      this.transform = {
        ...this.transform,
        scale: this.scale
      };
    }
  }

  zoomInIsInRange() {
    return this.scale >= 0.5 && this.scale < 6
  }

  onZoomChange(e) {
    this.scale = e.value;
    this.transform = {
      ...this.transform,
      scale: this.scale
    };
  }

  moveLeft() {
    this.transform = {
      ...this.transform,
      translateH: ++this.translateH
    };
  }

  moveRight() {
    this.transform = {
      ...this.transform,
      translateH: --this.translateH
    };
  }

  moveTop() {
    this.transform = {
      ...this.transform,
      translateV: ++this.translateV
    };
  }

  moveBottom() {
    this.transform = {
      ...this.transform,
      translateV: --this.translateV
    };
  }

  loadImageFailed() {
    this.displayCropDialog = false;
    console.error('Load image failed');
    this.messageService.add({ severity: 'error', summary: 'Error loading image from file.', detail: '' });
  }

  saveCroppedImage() {
    this.currentPicture = `url(${this.croppedImage})`;
    this.scale = 1;
    this.displayCropDialog = false;
  }
}
