import { Component, inject, input, OnInit, signal, ViewEncapsulation } from '@angular/core';
import { ValidationErrors } from '@angular/forms';
import { ValidationErrorsEnum } from '../../enums/validation-errors';
import { Router } from '@angular/router';
import { InputExtension } from './input-extension/input-extension';
import { NgTemplateOutlet } from '@angular/common';
export type InputType = 'text' | 'number' | 'password' | 'email' | 'tel' | 'date';

@Component({
  selector: 'app-input-wrapper',
  imports: [InputExtension, NgTemplateOutlet],
  encapsulation: ViewEncapsulation.None,
  templateUrl: './input-wrapper.html',
  styleUrl: './input-wrapper.scss',
})
export class InputWrapper implements OnInit {
  //errors from FormControl
  error = input<ValidationErrors | null | undefined>(null);
  type = input<InputType>('text');
  label = input<string | null>(null);
  styles = input<string>('');
  placeholder = input<string>('');
  labelStyles = input<string>('');
  labelPosition = input<'left' | 'top' | 'right' | 'inside'>('top');

  _labelStyles = '';
  ngOnInit() {
    this._labelStyles = `color: var(--app-cyan); font-size: 14px;${this.labelStyles()}`;
  }

  getErrorMessage() {
    let error = '';
     if (this.error()) {
      let key = Object.keys(this.error()!)[0];
      error = ValidationErrorsEnum[key](this.error()![key]);
    }

    return error;
  }

  //
  //
  //
  //for input extension
  //الازرار
  //
  icon = input<'refresh' | 'search' | 'add'>('add');
  link = input<string>('');
  handler = input<(() => void) | null>(null);
  router = inject(Router);
}
