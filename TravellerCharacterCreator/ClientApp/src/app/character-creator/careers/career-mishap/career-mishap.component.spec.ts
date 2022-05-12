import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerMishapComponent } from './career-mishap.component';

describe('CareerMishapComponent', () => {
  let component: CareerMishapComponent;
  let fixture: ComponentFixture<CareerMishapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerMishapComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerMishapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
