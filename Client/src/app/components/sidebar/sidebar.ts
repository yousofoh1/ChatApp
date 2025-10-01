import { Component, inject, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { UsersS } from '../../services/users/users-s';
import { IUser } from '../../services/auth/auth-service';
import { BaseComp } from '../BaseComp';

@Component({
  selector: 'app-sidebar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.scss',
})
export class Sidebar extends BaseComp implements OnInit {
  usersS = inject(UsersS);
  //
  users: IUser[] = [];
  ngOnInit() {
    this.usersS.getAll().subscribe({
      next: (res: IUser[]) => {
        let currentUserName = this.authS.user()?.userName;
        this.users = res.filter((user) => user.userName !== currentUserName);
      },
    });
  }
}
