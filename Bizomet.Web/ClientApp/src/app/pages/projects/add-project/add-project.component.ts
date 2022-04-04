import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Message } from 'primeng/api/message';
import { AppBreadcrumbService } from 'src/app/core/services/app.breadcrumb.service';
import { MenuItem, MessageService } from 'primeng/api';
import { ProjectService } from 'src/app/core/services/project.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectModel } from 'src/app/shared/models/project.model';
import { SharedData } from 'src/app/shared/shared-data.module';
import { KeyValuePairModel } from 'src/app/shared/models/key-value-pair.model';
import { FileUpload } from 'primeng/fileupload';
import { ProjectAttachmentModel } from 'src/app/shared/models/project-attachment.model';

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html'
})
export class AddProjectComponent implements OnInit {

  projectForm: FormGroup;
  errorMessage: Message[] = [];
  loading = false;
  project: ProjectModel;
  activeTabIndex: number = 0;
  stepsItems: MenuItem[];
  interview_conditions: KeyValuePairModel[] = SharedData.interview_conditions;
  interview_results: KeyValuePairModel[] = SharedData.interview_results;
  financial_types: KeyValuePairModel[] = SharedData.financial_types;
  minDate: Date = new Date();
  uploadedFiles: any[] = [];
  @ViewChild('fileInput') fileInput: FileUpload;
  private _returnUrl: string = "";

  constructor(
    private _router: Router,
    private route: ActivatedRoute,
    private projectService: ProjectService,
    private breadcrumbService: AppBreadcrumbService,
    private messageService: MessageService) {

    this.stepsItems = [{ label: 'General' }, { label: 'Attachments' }, { label: 'Contacts' }, { label: 'Confirmation' }];

    this.breadcrumbService.setItems([
      { label: 'Projects', routerLink: ['/projects'], icon: 'pi pi-fw pi-briefcase mr-1' },
      { label: 'New Project' }
    ]);

    this.projectForm = new FormGroup({
      title: new FormControl("", [Validators.required]),
      requestDate: new FormControl("", [Validators.required]),
      description: new FormControl("", [Validators.required]),
      interviewCondition: new FormControl("", [Validators.required]),
      interviewConditionComment: new FormControl(""),
      interviewResult: new FormControl("", [Validators.required]),
      interviewResultComment: new FormControl(""),
      wishContactedByMediaAssistant: new FormControl(""),
      mediaAssistantFinancialService: new FormControl(""),
      wishContactedByPromoter: new FormControl(""),
      promoterFinancialService: new FormControl(""),
      wishContactedByProducer: new FormControl(""),
      producerFinancialService: new FormControl(""),
      location: new FormControl(""),
      remoteLocation: new FormControl(""),
      dueDate: new FormControl(""),
      attachments: new FormControl("")
    });
  }

  ngOnInit(): void {
    this.project = new ProjectModel();
    this.project.id = "newId";
    this.project.requestDate = new Date();

    this._returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    this.errorMessage = [];
    this.loading = false;
    this.projectForm.controls["requestDate"].setValue(new Date());
    this.projectForm.controls["interviewResult"].setValue(this.interview_results[0].code);
    this.projectForm.controls["interviewCondition"].setValue(this.interview_conditions[0].code);
    this.projectForm.controls["wishContactedByMediaAssistant"].setValue(false);
    this.projectForm.controls["mediaAssistantFinancialService"].setValue(this.financial_types[0].code);
    this.projectForm.controls["wishContactedByPromoter"].setValue(false);
    this.projectForm.controls["promoterFinancialService"].setValue(this.financial_types[0].code);
    this.projectForm.controls["wishContactedByProducer"].setValue(false);
    this.projectForm.controls["producerFinancialService"].setValue(this.financial_types[0].code);
    this.projectForm.controls["location"].setValue('');
    this.projectForm.controls["remoteLocation"].setValue(false);
    this.projectForm.controls["attachments"].setValue([]);
    this.projectForm.controls["dueDate"].setValue(null);
  }

  activeIndexChange(index) {
    this.activeTabIndex = index;
  }

  public validateControl = (controlName: string) => {
    return this.projectForm.controls[controlName].invalid && this.projectForm.controls[controlName].touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.projectForm.controls[controlName].hasError(errorName)
  }

  customUpload(event) {
    event.files.forEach(file => {
      this.uploadedFiles.push(file);
    });
  }

  public saveAction = (formValue: any) => {
    this.loading = true;
    this.errorMessage = [];

    this.project.title = formValue.title;
    this.project.description = formValue.description;
    this.project.interviewCondition = formValue.interviewCondition;
    this.project.interviewConditionComment = formValue.interviewConditionComment;
    this.project.interviewResult = formValue.interviewResult;
    this.project.interviewResultComment = formValue.interviewResultComment;
    this.project.wishContactedByMediaAssistant = formValue.wishContactedByMediaAssistant;
    this.project.mediaAssistantFinancialService = formValue.mediaAssistantFinancialService;
    this.project.wishContactedByPromoter = formValue.wishContactedByPromoter;
    this.project.promoterFinancialService = formValue.promoterFinancialService;
    this.project.wishContactedByProducer = formValue.wishContactedByProducer;
    this.project.producerFinancialService = formValue.producerFinancialService;
    this.project.location = formValue.location;
    this.project.remoteLocation = formValue.remoteLocation;
    this.project.dueDate = formValue.dueDate;
    this.project.attachments = formValue.attachments;

    this.fileInput.upload();

    this.projectService.create(this.project)
      .subscribe((data: any) => {
        this.uploadedFiles.forEach(file => {
          let fileReader = new FileReader();
          fileReader.readAsDataURL(file);
          fileReader.onloadend = (error: any) => {
            //console.log(file);
            let attachment = new ProjectAttachmentModel();
            attachment.id = 'new_id';
            attachment.projectId = data.id;
            attachment.title = file.name;
            attachment.fileName = file.type;
            attachment.fileType = file.name;
            attachment.size = file.size;
            attachment.binaryContent =  fileReader.result;
            attachment.thumbnail = '';
            this.projectService.uploadAttachment(attachment)
              .subscribe(() => {
                //console.log("file uploaded");
              },
                (error: Error) => {
                  console.error(error);
                }
              );
          };
        },
          (error: Error) => {
            console.error(error);
          }
        );

        this.messageService.add({ severity: 'success', summary: 'Saved', detail: '' });
        this._router.navigate([this._returnUrl])
      });
  }

  public cancelAction = () => this._router.navigate([this._returnUrl]);
}
