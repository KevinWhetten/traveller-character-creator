import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerMishapsComponent } from './career-mishaps.component';

describe('CareerMishapsComponent', () => {
  let component: CareerMishapsComponent;
  let fixture: ComponentFixture<CareerMishapsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerMishapsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerMishapsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
