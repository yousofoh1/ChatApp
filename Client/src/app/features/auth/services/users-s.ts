import { Injectable } from '@angular/core';
import BaseService from '../../../services/base-service';
import { IUser } from './auth-s';

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
