import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventHobbyComponent } from './event-hobby.component';

describe('EventHobbyComponent', () => {
  let component: EventHobbyComponent;
  let fixture: ComponentFixture<EventHobbyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventHobbyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventHobbyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
