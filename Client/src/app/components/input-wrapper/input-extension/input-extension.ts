import { Component, inject, input, ViewEncapsulation } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-input-extension',
  imports: [],
  encapsulation: ViewEncapsulation.None,
  templateUrl: './input-extension.html',
  styleUrl: './input-extension.scss',
})
export class InputExtension {
  icon = input<'refresh' | 'search' | 'add'>('add');
  link = input('');
  handler = input<null | (() => void)>(null);
  router = inject(Router);

  handleClick(event: MouseEvent) {
    let isMouseEvent = event.type == 'click';
    
    if (!isMouseEvent) return;
    if (!this.link()) {
      this.handler()!();
    } else {
      this.router.navigate([this.link()]);
    }
  }
}
