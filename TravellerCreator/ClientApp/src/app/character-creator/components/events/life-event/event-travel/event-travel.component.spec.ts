import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventTravelComponent } from './event-travel.component';

describe('EventTravelComponent', () => {
  let component: EventTravelComponent;
  let fixture: ComponentFixture<EventTravelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventTravelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventTravelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
