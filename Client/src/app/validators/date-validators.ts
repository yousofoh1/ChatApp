import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function minDate(min: Date): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (!control.value) return null;
     let minDate = new Date(min.getFullYear(), min.getMonth(), min.getDate());
    const controlDate = new Date(control.value);
    if (isNaN(controlDate.getTime())) return null; // not a valid date

    return controlDate >= minDate ? null : { minDate: { required: minDate, actual: controlDate } };
  };
}

export function maxDate(max: Date): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (!control.value) return null;
    let maxDate = new Date(max.getFullYear(), max.getMonth(), max.getDate());

    const controlDate = new Date(control.value);
    if (isNaN(controlDate.getTime())) return null; // not a valid date

    return controlDate <= maxDate ? null : { maxDate: { required: maxDate, actual: controlDate } };
  };
}
