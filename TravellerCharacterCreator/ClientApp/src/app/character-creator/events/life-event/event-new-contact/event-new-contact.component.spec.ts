import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventNewContactComponent } from './event-new-contact.component';

describe('EventNewContactComponent', () => {
  let component: EventNewContactComponent;
  let fixture: ComponentFixture<EventNewContactComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventNewContactComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventNewContactComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
