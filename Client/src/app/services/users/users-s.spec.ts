import { TestBed } from '@angular/core/testing';

import { UsersS } from './users-s';

describe('UsersS', () => {
  let service: UsersS;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UsersS);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
