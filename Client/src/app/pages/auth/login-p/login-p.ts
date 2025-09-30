import { Component, inject } from '@angular/core';
import { ReactiveFormsModule, Validators } from '@angular/forms';
import { BaseComp } from '../../../components/BaseComp';
import { InputTextModule } from 'primeng/inputtext';
import { AuthService } from '../../../services/auth/auth-service';
import { InputWrapper } from '../../../components/input-wrapper/input-wrapper';

@Component({
  selector: 'app-login-p',
  imports: [ReactiveFormsModule, InputWrapper, InputTextModule],
  templateUrl: './login-p.html',
  styleUrl: './login-p.scss',
})
export class LoginP extends BaseComp {
  generalError: string | null = null;

  override initialForm = {
    email: ['yousofoh1@gmail.com', { validators: [Validators.required, Validators.email] }],
    password: ['P@ssw0rd', { validators: [Validators.required] }],
  };

  constructor() {
    super();
    this.InitializeForm();
  }

  onSubmit() {
    if (this.fg.invalid) {
      this.fg.markAllAsTouched();
      return;
    }

    this.authS.login(this.fg.value).subscribe({
      next: (res) => {
        this.router.navigateByUrl('');
        this.InitializeForm();
      },
      error: (err) => {
        this.generalError = err.error.message;
      },
    });
  }
}
