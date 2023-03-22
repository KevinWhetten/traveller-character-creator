import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventPrankComponent } from './event-prank.component';

describe('EventPrankComponent', () => {
  let component: EventPrankComponent;
  let fixture: ComponentFixture<EventPrankComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventPrankComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventPrankComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
