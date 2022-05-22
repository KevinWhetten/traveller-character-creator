import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventCrimeComponent } from './event-crime.component';

describe('EventCrimeComponent', () => {
  let component: EventCrimeComponent;
  let fixture: ComponentFixture<EventCrimeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventCrimeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventCrimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
