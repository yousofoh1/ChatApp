import { TestBed } from '@angular/core/testing';

import { LayoutS } from './layout-s';

describe('LayoutS', () => {
  let service: LayoutS;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LayoutS);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
