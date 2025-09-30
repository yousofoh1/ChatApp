import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Menu } from 'primeng/menu';
import { ButtonModule } from 'primeng/button';
import { BaseComp } from '../BaseComp';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [Menu, ButtonModule, RouterLink],
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header extends BaseComp {
  accountMenu: MenuItem[] | undefined;
  user=this.authS.user
  ngOnInit() {
    this.accountMenu = [
      {
        label: 'Options',
        items: [
          {
            label: 'Log out',
            icon: 'pi pi-sign-out',
            command: () => {
              this.authS.logout();
            }
          },
        ],
      },
    ];
  }
}
