import {
  Directive,
  EventEmitter,
  HostListener,
  Input,
  Output,
} from '@angular/core';
import { debounceTime, Subject } from 'rxjs';

@Directive({
  selector: '[appDebounce]'
})
export class Debounce {

  @Input() debounceTime = 300;
  @Output() debounced = new EventEmitter();

  private clicks = new Subject<void>();

  constructor() {
    this.clicks
      .pipe(debounceTime(this.debounceTime))
      .subscribe(() => this.debounced.emit());
  }

  @HostListener('click', ['$event'])
  onClick(e: Event) {
    if (e.target instanceof HTMLInputElement) {
      return;
    }
    this.clicks.next();
  }

  @HostListener('keyup.enter', ['$event'])
  @HostListener('input', ['$event'])
  onChange(e: Event) {
    if ((e.target as HTMLInputElement).value.trim() == '') {
      return;
    }
    this.clicks.next();
  }

}
