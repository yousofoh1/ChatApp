import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { tap } from 'rxjs';

export interface IUser {
  id: string;
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  imageUrl: string;
}

function getUser() {
  if (localStorage.getItem('user'))
    return JSON.parse(localStorage.getItem('user')!) as IUser;
  return null;
}

export enum AuthStagesEnum {
  login = 0,
  forgotPassword,
  otp,
  resetPassword,
}
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  isLoggedIn = localStorage.getItem('token') ? true : false;
  currentStage = signal(AuthStagesEnum.login);
  httpClient = inject(HttpClient);
  user = signal<IUser | null>(getUser());
  router = inject(Router);
  rootUrl = environment.rootUrl + '/api';
  code = signal<string[]>(['', '', '', '', '', '']);
  _token = '';
  _userId = '';

  //

  mailForOtp = '';
  login(value: any) {
    return this.httpClient
      .post(`${this.rootUrl}/auth/login`, value, {
        headers: { 'skip-error': 'true' },
      })
      .pipe(
        tap({
          next: (res: any) => {
            this.isLoggedIn = true;
            this.router.navigateByUrl('');
            this.user.set(res.user);
            localStorage.setItem('token', res.token);
            localStorage.setItem('user', JSON.stringify(res.user));
          },
        })
      );
  }

  get userId() {
    return localStorage.getItem('userId') ?? this._userId;
  }
  get token() {
    return localStorage.getItem('token') ?? this._token;
  }
  // set userId(id: string) {
  //   localStorage.setItem('userId', id);
  // }

  openForgotPassword() {
    this.currentStage.set(AuthStagesEnum.forgotPassword);
  }

  openResetPassword() {
    this.currentStage.set(AuthStagesEnum.resetPassword);
  }

  sendOtp(email: string) {
    return this.httpClient
      .post(`${this.rootUrl}/User/sendOtpByEmail`, {
        email,
      })
      .pipe(
        tap({
          next: () => {
            this.currentStage.set(AuthStagesEnum.otp);
          },
        })
      );
  }

  openLogin() {
    this.currentStage.set(AuthStagesEnum.login);
  }

  logout() {
    this.isLoggedIn = false;
    this.user.set(null);
    localStorage.removeItem('user');
    localStorage.removeItem('token');
    this.router.navigateByUrl('/auth/login');
    this.currentStage.set(AuthStagesEnum.login);
  }
}
