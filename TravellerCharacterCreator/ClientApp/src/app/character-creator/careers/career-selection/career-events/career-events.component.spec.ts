import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerEventsComponent } from './career-events.component';

describe('CareerEventsComponent', () => {
  let component: CareerEventsComponent;
  let fixture: ComponentFixture<CareerEventsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerEventsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerEventsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
