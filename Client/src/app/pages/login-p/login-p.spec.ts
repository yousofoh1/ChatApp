import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginP } from './login-p';

describe('LoginP', () => {
  let component: LoginP;
  let fixture: ComponentFixture<LoginP>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoginP]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoginP);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
