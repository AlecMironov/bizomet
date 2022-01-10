import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-menu',
  templateUrl: './login-menu.component.html',
  styleUrls: ['./login-menu.component.css']
})
export class LoginMenuComponent implements OnInit {
  public IsAuthenticated: boolean = false;
  public userName?: string;

  constructor(private _authService: AuthenticationService, private _router: Router) 
  {
  }

  ngOnInit(): void {
    this._authService.authChanged
    .subscribe(res => {
      this.IsAuthenticated = res;
    });
    this.userName = "USER_NAME";
    //this.userName = this._authService.getUser().pipe(map(u => u && u.name));
  }
}
