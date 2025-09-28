import { Component, inject } from '@angular/core';
import { ReactiveFormsModule, Validators } from '@angular/forms';
import { BaseComp } from '../../../components/BaseComp';
import { AuthService } from '../../../services/auth/auth-service';
import { InputExtension } from '../../../components/input-wrapper/input-extension/input-extension';
import { InputWrapper } from '../../../components/input-wrapper/input-wrapper';

@Component({
  selector: 'app-login-p',
  imports: [ReactiveFormsModule, InputWrapper],
  templateUrl: './login-p.html',
  styleUrl: './login-p.css',
})
export class LoginP extends BaseComp {
  authS = inject(AuthService);
  error = '';

  override initialForm = {
    email: ['', { validators: [Validators.required, Validators.email] }],
    password: ['', { validators: [Validators.required] }],
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
        this.error = err.error.message;
      },
    });
  }
}
