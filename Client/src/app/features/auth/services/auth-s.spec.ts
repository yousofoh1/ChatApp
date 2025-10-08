import { TestBed } from '@angular/core/testing';

import { AuthS } from './auth-s';

describe('AuthS', () => {
  let service: AuthS;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthS);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
