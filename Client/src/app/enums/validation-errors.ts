import { minDate } from "../validators/date-validators";

export const ValidationErrorsEnum: any = {
  required() {
    return 'This field is required';
  },
  minlength({ requiredLength, actualLength }: { requiredLength: number; actualLength: number }) {
    return `The minimum length for this field is ${requiredLength}`;
  },
  maxlength({ requiredLength, actualLength }: { requiredLength: number; actualLength: number }) {
    return `The maximum length for this field is ${requiredLength}`;
  },
  minDate({ required, actual }: { required: Date; actual: Date }) {
    return `The entered value is less than the minimum allowed (${required.toISOString().split('T')[0]})`;
  },
  maxDate({ required, actual }: { required: Date; actual: Date }) {
    return `The entered value is greater than the maximum allowed (${required.toISOString().split('T')[0]})`;
  },
  min({ min, actual }: { min: number; actual: string }) {
    return `The entered value is less than the minimum allowed (${min})`;
  },
  max({ max, actual }: { max: number; actual: string }) {
    return `The entered value is greater than the maximum allowed (${max})`;
  },
  email() {
    return 'Invalid email address';
  },
  pattern() {
    return 'Invalid value';
  },
  mask() {
    return 'Invalid value';
  },
  lessThanPurchasePrice() {
    return 'The selling price must be greater than the purchase price';
  }
};

