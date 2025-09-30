import { TestBed } from '@angular/core/testing';

import { SignalRS } from './signal-r-s';

describe('SignalRS', () => {
  let service: SignalRS;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SignalRS);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
