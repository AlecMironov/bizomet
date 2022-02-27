import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Message, MessageService } from 'primeng/api';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { NavigationService } from 'src/app/core/services/navigation.service';
import { RepositoryService } from 'src/app/core/services/repository.service';
import { KeyValuePairModel } from 'src/app/shared/models/key-value-pair.model';
import { ContactUsRequestModel } from 'src/app/shared/models/contact-us-request.model';

@Component({
  selector: 'app-contact-us',
  templateUrl: 'contact-us.component.html'
})
export class ContactUsComponent implements OnInit {
  contactUsForm: FormGroup;
  errorMessage: Message[] = [];
  loading: boolean;
  contactReasons: KeyValuePairModel[];
  selectedReason: KeyValuePairModel;
  token: string;

  constructor(
    private repositoryService: RepositoryService,
    private authService: AuthenticationService,
    private navigation: NavigationService,
    private messageService: MessageService) {

    this.contactUsForm = new FormGroup({
      firstName: new FormControl("", [Validators.required]),
      lastName: new FormControl("", [Validators.required]),
      email: new FormControl("", [Validators.required]),
      phoneNumber: new FormControl(""),
      contactReason: new FormControl("", [Validators.required]),
      description: new FormControl("", [Validators.required])
    });
  }

  showResponse(response) {
    this.token = response;
  }

  ngOnInit(): void {
    //this.addScript();
    this.errorMessage = [];
    this.loading = true;

    this.repositoryService.getAll<any>("common/contactusreason", null)
      .subscribe(res => {
        this.contactReasons = res;
        this.contactUsForm.controls["firstName"].setValue(this.authService.currentUser?.firstName);
        this.contactUsForm.controls["lastName"].setValue(this.authService.currentUser?.lastName);
        this.contactUsForm.controls["email"].setValue(this.authService.currentUser?.email);
        this.contactUsForm.controls["phoneNumber"].setValue(this.authService.currentUser?.phoneNumber);
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

  // addScript() {
  //   let script = document.createElement('script');
  //   script.src = 'https://www.google.com/recaptcha/api.js?render=explicit&onload=initRecaptcha';
  //   script.async = true;
  //   script.defer = true;
  //   document.body.appendChild(script);
  // }

  public submitAction = (formValue: any) => {
    this.loading = true;
    this.errorMessage = [];

    let contactUsRequestModel: ContactUsRequestModel = {
      userId: this.authService.currentUser?.id,
      reason: formValue.contactReason,
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      email: formValue.email,
      phoneNumber: formValue.phoneNumber,
      description: formValue.description,
      captcha: this.token
    }

    this.repositoryService.create<ContactUsRequestModel>('common/contactussubmit', contactUsRequestModel)
      .subscribe(() => {
        this.messageService.add({ severity: 'success', summary: 'Successful submit', detail: 'Your message has been submitted.' });
        this.loading = false;
        setTimeout(() => this.navigation.back(), 2000); // 2 seconds to see the successful message
      });
  }
}
