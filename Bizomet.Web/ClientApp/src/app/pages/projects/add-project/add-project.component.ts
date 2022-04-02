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
    this.projectForm.controls["attachments"].setValue([]);
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

  myUploader(event) {
    event.files.forEach(file => {
      this.uploadedFiles.push(file);
    });
  }

  onFileSelect(event) {
    console.log("onFileSelect", event);
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
    this.project.dueDate = formValue.dueDate;

    this.fileInput.upload();

    this.uploadedFiles.forEach(file => {
      let myReader: FileReader = new FileReader();
      myReader.readAsText(file);
      myReader.onloadend = (error: any): void => {
        console.log(file);
        console.log(myReader.result);
        // this.uploadService.postClasses(myReader.result).subscribe(
        //   (data: any): void => {
        //     if(data.success) {
        //       console.log(data);
        //       this.flashMessage.show('Upload Classes Successfully', {
        //           cssClass: 'alert-success',
        //           timeout: 3000
        //       });
        //       this.router.navigate(['/uploadclass']);
        //     } else {
        //       this.msgs = [];
        //       this.msgs.push({severity: 'error', summary: 'Upload Error', detail: 'Something went wrong'});
        //       this.router.navigate(['/uploadclass']);
        //     }
        //   },
        //   (error: Error): void =>{
        //     console.error(error);
        //   }
        //);
      };
    });

    this.projectService.create(this.project)
      .subscribe(() => {
        this.messageService.add({ severity: 'success', summary: 'Saved', detail: '' });
        this._router.navigate([this._returnUrl])
      });
  }

  public cancelAction = () => this._router.navigate([this._returnUrl]);
}
