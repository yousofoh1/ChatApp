import { minDate } from "../validators/date-validators";

export const ValidationErrorsEnum : any = {
  required(){
    return 'هذا الحقل مطلوب';
  },
  minlength({ requiredLength, actualLength }: { requiredLength: number; actualLength: number }) {
    return `يجب تعبئة هذا الحقل بحد اقل ${requiredLength}`;
  },
  maxlength({ requiredLength, actualLength }: { requiredLength: number; actualLength: number }) {
    return `يجب تعبئة هذا الحقل بحد اقصي ${requiredLength}`;
  },
  minDate({ required, actual }: { required: Date; actual: Date }) {

    return `القيمة المدخلة اقل من الحد الادنى (${required.toISOString().split('T')[0]})`;
  },
  maxDate({ required, actual }: { required: Date; actual: Date }) {
    console.log(required, actual);
    return `القيمة المدخلة أكبر من الحد الأقصى (${required.toISOString().split('T')[0]})`;
  },
  min({ min, actual }: { min: number; actual: string }) {
    return `القيمة المدخلة اقل من الحد الادنى (${min})`;
  },
  max({ max, actual }: { max: number; actual: string }) {
    return `القيمة المدخلة أكبر من الحد الأقصى (${max})`;
  },
  email(){
    return 'البريد الإلكتروني غير صالح'
  } ,
  pattern(){
    return 'القيمة غير صالحة'
  },
  mask(){
    return 'القيمة غير صالحة'
  },
  lessThanPurchasePrice(){
    return 'سعر البيع يجب أن يكون أكبر من سعر الشراء';
  }
};
