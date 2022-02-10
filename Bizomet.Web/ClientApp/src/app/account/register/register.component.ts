import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Message, MessageService } from 'primeng/api';
import { PasswordConfirmationValidatorService } from '../../shared/custom-validators/password-confirmation-validator.service';
import { UserRegistrationModel } from '../../shared/models/user-registration.model';
import { AuthenticationService } from 'src/app/core/services/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  providers: [MessageService]
})
export class RegisterComponent implements OnInit {
  features: any[];
  public registerForm!: FormGroup;
  public errorMessage: Message[] = [];
  public disablePage = false;
  public registrationCompleted = false;

  constructor(
    private _authenticationService: AuthenticationService,
    private _passConfValidator: PasswordConfirmationValidatorService,
    @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit(): void {
    this.features = [
      { title: 'Unlimited Inbox', image: 'live-collaboration.svg', text: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.' },
      { title: 'Data Security', image: 'security.svg', text: 'Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.' },
      { title: 'Cloud Backup Williams', image: 'subscribe.svg', text: 'Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.' }
    ];

    this.registerForm = new FormGroup({
      firstname: new FormControl('', [Validators.required]),
      lastname: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      confirm: new FormControl('')
    });

    this.registerForm?.get('confirm')?.setValidators([Validators.required,
    this._passConfValidator.validateConfirmPassword(this.registerForm?.get('password')!)]);
  }

  public validateControl = (controlName: string) => {
    return this.registerForm.controls[controlName].invalid && this.registerForm.controls[controlName].touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.registerForm.controls[controlName].hasError(errorName)
  }

  public registerUser = (registerFormValue: any) => {
    this.disablePage = true;
    this.errorMessage = [];
    const formValues = { ...registerFormValue };

    const user: UserRegistrationModel = {
      firstname: formValues.firstname,
      lastname: formValues.lastname,
      email: formValues.email,
      password: formValues.password,
      confirmPassword: formValues.confirm,
      clientURI: `${this.baseUrl}account/emailconfirmation`
    };

    this._authenticationService.register(user)
      .subscribe(_ => {
        this.disablePage = false;
        this.registrationCompleted = true;
      },
        error => {
          this.disablePage = false;
          this.registrationCompleted = false;
          this.errorMessage = [
            { severity: 'error', summary: '', detail: error }
          ];
        })
  }
}
