import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventPoliticalComponent } from './event-political.component';

describe('EventPoliticalComponent', () => {
  let component: EventPoliticalComponent;
  let fixture: ComponentFixture<EventPoliticalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventPoliticalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventPoliticalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
