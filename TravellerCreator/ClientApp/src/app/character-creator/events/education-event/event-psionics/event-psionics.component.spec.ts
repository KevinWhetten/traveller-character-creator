import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventPsionicsComponent } from './event-psionics.component';

describe('EventPsionicsComponent', () => {
  let component: EventPsionicsComponent;
  let fixture: ComponentFixture<EventPsionicsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventPsionicsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventPsionicsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
