import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventUnusualEventComponent } from './event-unusual-event.component';

describe('EventUnusualEventComponent', () => {
  let component: EventUnusualEventComponent;
  let fixture: ComponentFixture<EventUnusualEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventUnusualEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventUnusualEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
