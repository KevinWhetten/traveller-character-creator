import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventTragedyComponent } from './event-tragedy.component';

describe('EventTragedyComponent', () => {
  let component: EventTragedyComponent;
  let fixture: ComponentFixture<EventTragedyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventTragedyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventTragedyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
