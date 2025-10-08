import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeP } from './home-p';

describe('HomeP', () => {
  let component: HomeP;
  let fixture: ComponentFixture<HomeP>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomeP]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HomeP);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
