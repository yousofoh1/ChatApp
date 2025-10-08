import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MessagingP } from './messaging-p';

describe('MessagingP', () => {
  let component: MessagingP;
  let fixture: ComponentFixture<MessagingP>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MessagingP]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MessagingP);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
