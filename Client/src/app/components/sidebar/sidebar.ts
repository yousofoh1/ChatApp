import { Component, inject, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { UsersS } from '../../services/users/users-s';
import { IUser } from '../../services/auth/auth-service';

@Component({
  selector: 'app-sidebar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.scss',
})
export class Sidebar implements OnInit {
  usersS = inject(UsersS);
  //
  users: IUser[] = [];
  ngOnInit() {
    this.usersS.getAll().subscribe({
      next: (res) => {
        this.users = res;
      },
    });
  }
}
