import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InputExtension } from './input-extension';

describe('InputExtension', () => {
  let component: InputExtension;
  let fixture: ComponentFixture<InputExtension>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InputExtension]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InputExtension);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
