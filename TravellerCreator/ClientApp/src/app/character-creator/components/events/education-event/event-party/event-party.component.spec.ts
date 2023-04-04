import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventPartyComponent } from './event-party.component';

describe('EventPartyComponent', () => {
  let component: EventPartyComponent;
  let fixture: ComponentFixture<EventPartyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventPartyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventPartyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
