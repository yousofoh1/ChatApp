import { Component, input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ValidationErrorsEnum } from '../../enums/validation-errors';
import { NgTemplateOutlet } from '@angular/common';
import { MessageModule } from 'primeng/message';


@Component({
  selector: 'app-input-wrapper',
  imports: [NgTemplateOutlet,MessageModule],
  templateUrl: './input-wrapper.html',
  styleUrl: './input-wrapper.scss',
})
export class InputWrapper {
  fc = input<FormControl | null >(null);


  getErrorMessage() {
    let error = '';
     if (this.fc()?.errors) {
      let key = Object.keys(this.fc()?.errors!)[0];
      error = ValidationErrorsEnum[key](this.fc()?.errors![key]);
    }
    return error;
  }

  
}
