import { AuthService } from '@abp/ng.core';
import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  constructor(
    private oAuthService: OAuthService, private authService: AuthService
  ) {
  }

  get hasLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();
  }

  login() {
    this.authService.initLogin();
  }
}
