import { UserAuthenticationModel } from '../../shared/models/user-authentication.model';
import { Router, ActivatedRoute } from '@angular/router';
import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { AppComponent } from 'src/app/app.component';
import { Message, MessageService } from 'primeng/api';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  providers: [MessageService]
})
export class LoginComponent implements OnInit {
  features: any[];
  public loginForm: FormGroup;
  public errorMessage: Message[] = [];
  public loading = false;
  private _returnUrl: string = "";

  constructor(
    public app: AppComponent,
    private authenticationService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute,
    @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit(): void {
    this.features = [
      { title: 'Unlimited Inbox', image: 'live-collaboration.svg', text: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.' },
      { title: 'Data Security', image: 'security.svg', text: 'Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.' },
      { title: 'Cloud Backup Williams', image: 'subscribe.svg', text: 'Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.' }
    ];

    this.loginForm = new FormGroup({
      username: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required])
    })

    this._returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  public validateControl = (controlName: string) => {
    return this.loginForm.controls[controlName].invalid && this.loginForm.controls[controlName].touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.loginForm.controls[controlName].hasError(errorName)
  }

  public loginAction = (loginFormValue: any) => {
    this.loading = true;
    this.errorMessage = [];
    const login = { ...loginFormValue };
    const model: UserAuthenticationModel = {
      userName: login.username,
      password: login.password,
      clientURI: `${this.baseUrl}account/forgotpassword`
    }

    this.authenticationService.login(model)
      .subscribe(res => {
        this.loading = false;

        if (this._returnUrl === "/") {
          this.router.navigate(["/inquiries"]);
        }
        else {
          this.router.navigate([this._returnUrl]);
        }
      },
        (error) => {
          this.loading = false;
          this.errorMessage = [
            { severity: 'error', summary: '', detail: error }
          ];
        });
  }
}
