import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerEventComponent } from './career-event.component';

describe('CareerEventComponent', () => {
  let component: CareerEventComponent;
  let fixture: ComponentFixture<CareerEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
