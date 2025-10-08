import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LayoutS {
  navOpen = signal(true);
  language = signal(localStorage.getItem('lang') || 'en');
  isLoading  = signal(false);

  openNav() {
    //console.log(this.navOpen());
    this.navOpen.set(true);
  }
  closeNav() {
    this.navOpen.set(false);
  }
  toggleNav() {
    this.navOpen.set(!this.navOpen());
  }

  start() {
    this.isLoading.set(true);
  }

  stop() {
    this.isLoading.set(false);
  }
}
