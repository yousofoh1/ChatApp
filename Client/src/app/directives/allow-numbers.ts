import { Directive, HostListener, Input } from '@angular/core';
// import { ToastrService } from 'ngx-toastr';
import { BaseComp } from '../components/BaseComp';

@Directive({
  selector: '[appAllowNumbers]',
})
export class AllowNumbers extends BaseComp {
  constructor() {
    super();
  }


  @Input() max: number=Infinity;
  @Input() min: number=0;
  
  @HostListener('keydown', ['$event'])
  onKeyDown(event: KeyboardEvent) {
    // allow control keys (backspace, delete, arrows, tab, etc.)
    if (
      ['Backspace', 'Delete', 'ArrowLeft', 'ArrowRight', 'Tab'].includes(event.key)
    ) {
      return;
    }

    // block if not a number
    if (!/^[0-9]$/.test(event.key)) {
      event.preventDefault();
    }

    // block if value exceed max
    const input = event.target as HTMLInputElement;
    const currentValue = Number(input.value + event.key);
    if (currentValue > this.max) {
      console.log('exceed max',currentValue,this.max);
      // let message=this.localize(`الحد الاقصى هو ${this.max}`,`Max value is ${this.max}`);
      // this.toaster.warning(message);
      event.preventDefault();
    }
    if (currentValue < this.min) {
      // let message=this.localize(`لا يمكن ادخال قيمة اقل من ${this.min}`,`Value cannot be less than ${this.min}`);
      // this.toaster.warning(message);
      event.preventDefault();
    }

  }
}
