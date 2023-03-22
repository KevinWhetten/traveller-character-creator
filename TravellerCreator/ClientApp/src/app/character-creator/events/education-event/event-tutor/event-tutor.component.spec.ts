import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventTutorComponent } from './event-tutor.component';

describe('EventTutorComponent', () => {
  let component: EventTutorComponent;
  let fixture: ComponentFixture<EventTutorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventTutorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventTutorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
