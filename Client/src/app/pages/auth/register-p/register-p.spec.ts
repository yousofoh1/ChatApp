import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterP } from './register-p';

describe('RegisterP', () => {
  let component: RegisterP;
  let fixture: ComponentFixture<RegisterP>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterP]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterP);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
