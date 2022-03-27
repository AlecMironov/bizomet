import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Message } from 'primeng/api/message';
import { AppBreadcrumbService } from 'src/app/core/services/app.breadcrumb.service';
import { MessageService } from 'primeng/api';
import { InquiryService } from 'src/app/core/services/inquiry.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-add-inquiry',
  templateUrl: './add-inquiry.component.html'
})
export class AddInquiryComponent implements OnInit {

  inquiryForm: FormGroup;
  errorMessage: Message[] = [];
  loading = false;
  private _returnUrl: string = "";

  constructor(
    private _router: Router,
    private route: ActivatedRoute,
    private inquiryService: InquiryService,
    private breadcrumbService: AppBreadcrumbService,
    private messageService: MessageService) {

    this.breadcrumbService.setItems([
      { label: 'Inquiries', routerLink: ['/inquiries'], icon: 'pi pi-fw pi-briefcase mr-1' },
      { label: 'New Inquiry' }
    ]);

    this.inquiryForm = new FormGroup({
      title: new FormControl("", [Validators.required]),
      requestDate: new FormControl("", [Validators.required]),
      description: new FormControl("", [Validators.required])
    });
  }

  ngOnInit(): void {
    this._returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    this.errorMessage = [];
    this.loading = false;
  }

  public validateControl = (controlName: string) => {
    return this.inquiryForm.controls[controlName].invalid && this.inquiryForm.controls[controlName].touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.inquiryForm.controls[controlName].hasError(errorName)
  }

  public saveAction = (formValue: any) => {
    this.loading = true;
    this.errorMessage = [];
    var dirtyValues = this.getDirtyValues(this.inquiryForm);

    if (JSON.stringify(dirtyValues) != '{}') {
      // this.profileService.updateProfile(dirtyValues)
      //   .subscribe(() => {
      //     this.refreshRequired = true;
      //     this.ngOnInit();
      //     this.messageService.add({ severity: 'success', summary: 'Saved', detail: '' });
      //   });
    } else {
      this.loading = false;
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

  public cancelAction = () => this._router.navigate([this._returnUrl]);
}
