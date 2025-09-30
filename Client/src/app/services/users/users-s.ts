import { Injectable } from '@angular/core';
import BaseService from '../base-service';
import { IUser } from '../auth/auth-service';

@Injectable({
  providedIn: 'root'
})
export class UsersS extends BaseService<IUser> {
    /**
     *
     */
    constructor() {
      super("users");
      
    }
}
