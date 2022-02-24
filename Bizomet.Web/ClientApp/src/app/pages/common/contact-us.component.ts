import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Message } from 'primeng/api';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { RepositoryService } from 'src/app/core/services/repository.service';
import { KeyValuePairModel } from 'src/app/shared/models/key-value-pair.model';

@Component({
  selector: 'app-contact-us',
  templateUrl: 'contact-us.component.html'
})
export class ContactUsComponent {

  lang: string = 'en';
  contactUsForm: FormGroup;
  errorMessage: Message[] = [];
  loading: boolean;
  contactReasons: KeyValuePairModel[];
  selectedReason: KeyValuePairModel;

  constructor(private repositoryService: RepositoryService, private authService: AuthenticationService) {
    this.contactUsForm = new FormGroup({
      firstName: new FormControl("", [Validators.required]),
      lastName: new FormControl("", [Validators.required]),
      email: new FormControl("", [Validators.required]),
      phone: new FormControl(""),
      contactReason: new FormControl("", [Validators.required]),
      description: new FormControl("")
    });
  }

  ngOnInit(): void {
    this.addScript();

    this.errorMessage = [];

    this.loading = true;

    this.repositoryService.getAll<any>("common/contactusreason", null)
      .subscribe(res => {
        this.contactReasons = res;

        this.contactUsForm.controls["firstName"].setValue(this.authService.currentUser?.firstName);
        this.contactUsForm.controls["lastName"].setValue(this.authService.currentUser?.lastName);
        this.contactUsForm.controls["email"].setValue(this.authService.currentUser?.email);
        this.contactUsForm.controls["phone"].setValue(this.authService.currentUser?.phoneNumber);
        this.contactUsForm.controls["description"].setValue("");
        this.contactUsForm.controls["contactReason"].setValue(this.contactReasons[0]);

        this.selectedReason = this.contactReasons[0];
        this.loading = false;
      });
  }

  public validateControl = (controlName: string) => {
    return this.contactUsForm.controls[controlName].invalid && this.contactUsForm.controls[controlName].touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.contactUsForm.controls[controlName].hasError(errorName)
  }
  
  showResponse(event) {
    console.log("reCaptha Result: ", event);
  }

  addScript() {
    let script = document.createElement('script');
    const lang = this.lang ? '&hl=' + this.lang : '';
    script.src = `https://www.google.com/recaptcha/api.js?onload=initRecaptcha&render=explicit${lang}`;
    script.async = true;
    script.defer = true;
    document.body.appendChild(script);
  }

  public submitAction = (formValue: any) => {
    this.loading = true;
    this.errorMessage = [];
    // var dirtyValues = this.getDirtyValues(this.aboutMeForm);

    // if (this.currentPicture != this.originalPicture) {
    //   const pic = typeof this.currentPicture != 'undefined' && this.currentPicture ? this.currentPicture.substring(26, this.currentPicture.length - 1) : "";
    //   dirtyValues["picture"] = pic;
    // }

    // if (Object.prototype.hasOwnProperty.call(dirtyValues, 'roles')) {
    //   let originalValue = dirtyValues["roles"];
    //   dirtyValues["roles"] = originalValue.map((r: UserRole) => r.key);
    // }

    // if (JSON.stringify(dirtyValues) != '{}') {
    //   this.profileService.updateProfile(dirtyValues)
    //     .subscribe(() => {
    //       this.refreshRequired = true;
    //       this.ngOnInit();
    //       this.messageService.add({ severity: 'success', summary: 'Saved', detail: '' });
    //     });
    // } else {
    //   this.disablePage = false;
    // }
    this.loading = false;
  }
}
