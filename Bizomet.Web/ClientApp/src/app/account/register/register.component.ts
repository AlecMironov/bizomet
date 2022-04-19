import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MenuItem, Message, MessageService } from 'primeng/api';
import { UserRegistrationModel } from '../../shared/models/user-registration.model';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { CustomvalidationService } from 'src/app/core/services/custom-validation-service';
import { SharedData } from 'src/app/shared/shared-data.module';
import { UserRole } from 'src/app/shared/models/user-role.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  providers: [MessageService]
})
export class RegisterComponent implements OnInit {
  features: any[];
  public registerForm: FormGroup;
  public errorMessage: Message[] = [];
  public loading = false;
  public registrationCompleted = false;
  public stepsItems: MenuItem[];
  public activeTabIndex: number = 0;
  public roleList: UserRole[];
  public roleDescription: string[];
  token: string;

  constructor(
    private _authenticationService: AuthenticationService,
    private _customValidator: CustomvalidationService,
    @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit(): void {
    this.roleList = SharedData.all_roles;

    this.roleDescription = [
      `
      You are a talent and obviously have a lot of useful information to share about yourself, your profession, your hobby,
      whatever you want to speak about publicly.<br><br><span class="font-italic">(Example: you are a confectioner and want to
      talk about unique and gorgeous cakes you make)</span>`,
      `
      We have a person (TALENT) to tell his/her story, to promote some busines, service, or product. For this purpose, we need someone who can interview our talent.
      It could be an audio/video blogger, or anyone who can create an article or text for websites and social networks.
      It maybe newspaper/magazine journalist, people working on TV, or radio stations as hosts.<br><br>
      If you are one of those we mentioned above, or you are the one who have ability to
      interview someone then this role of our platform is for you.
      `,
      `
      We have a person who wanted to give an interview and people who interviewed them. As a result, we have a conversation in form of recorded phone call, video interview
      done via Skype, Zoom, audio recordings of any sort, etc.
      <br><br>
      It’s a time for you to shine if you are: text editor, journalist, blogger, video editor, anyone who can turn a recorded conversation to
      website/newspaper/magazine article, video, or audio podcast for social networks, tv programs, radio stations, etc.
      `,
      `
      If you work in advertisement industry, if you are SMM specialist this role is for you.<br><br>
      We have a Talent, and an interviewer (Uplifter), and we have managed to create
      professionally done content (video, article, podcast). Now we want the world to
      see our content. What that means in short, we need you, people who can promote our content.
      `,
      `
      Everyone must promote his business, product or service, but there are always
      people who don’t have a time or desire to find Lifter (Interviewer), to contact
      Media Assistant (text editor, video editor, etc.), or look for companies which
      will promote final media product (interview).<br><br>
      Here is where a producer will do everything or of the whole process for you.
      `
    ]

    this.stepsItems = [{ label: 'Roles' }, { label: 'Account' }, { label: 'Confirmation' }];

    this.registerForm = new FormGroup({
      role: new FormControl('', [Validators.required]),
      firstname: new FormControl('', [Validators.required]),
      lastname: new FormControl('', [Validators.required]),
      publicname: new FormControl('', [Validators.required], this._customValidator.publicNameValidator.bind(this._customValidator)),
      email: new FormControl('', [Validators.required, Validators.email]),
      username: new FormControl('', [Validators.required], this._customValidator.userNameValidator.bind(this._customValidator)),
      password: new FormControl('', Validators.compose([Validators.required, this._customValidator.patternValidator()])),
      confirm: new FormControl('')
    });

    this.registerForm?.get('confirm')?.setValidators([
      Validators.required,
      this._customValidator.validateConfirmPassword(this.registerForm?.get('password')!)
    ]);

    this.registerForm.controls["role"].setValue(this.roleList[0]);
  }

  showResponse(response) {
    this.token = response;
  }

  activeIndexChange(index) {
    this.activeTabIndex = index;
  }

  onRoleClick(roleIndex) {
    this.registerForm.controls["role"].setValue(this.roleList[roleIndex]);
  }

  validateControl = (controlName: string) => {
    return this.registerForm.controls[controlName].invalid && this.registerForm.controls[controlName].touched
  }

  hasError = (controlName: string, errorName: string) => {
    return this.registerForm.controls[controlName].hasError(errorName)
  }

  public registerUser = (registerFormValue: any) => {
    this.loading = true;
    this.errorMessage = [];
    const formValues = { ...registerFormValue };

    const user: UserRegistrationModel = {
      firstname: formValues.firstname,
      lastname: formValues.lastname,
      username: formValues.username,
      publicname: formValues.publicname,
      email: formValues.email,
      role: formValues.role.key,
      password: formValues.password,
      confirmPassword: formValues.confirm,
      clientURI: `${this.baseUrl}account/emailconfirmation`,
      captcha: this.token
    };

    this._authenticationService.register(user)
      .subscribe(_ => {
        this.loading = false;
        this.registrationCompleted = true;
        this.activeTabIndex = 2;
      },
        error => {
          this.loading = false;
          this.activeTabIndex = 1;
          this.registrationCompleted = false;
          this.errorMessage = [
            { severity: 'error', summary: '', detail: error }
          ];
        })
  }
}
