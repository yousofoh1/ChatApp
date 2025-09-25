import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmEmailP } from './confirm-email-p';

describe('ConfirmEmailP', () => {
  let component: ConfirmEmailP;
  let fixture: ComponentFixture<ConfirmEmailP>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConfirmEmailP]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfirmEmailP);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
