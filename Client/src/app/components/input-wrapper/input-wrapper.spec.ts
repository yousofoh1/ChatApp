import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InputWrapper } from './input-wrapper';

describe('InputWrapper', () => {
  let component: InputWrapper;
  let fixture: ComponentFixture<InputWrapper>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InputWrapper]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InputWrapper);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
