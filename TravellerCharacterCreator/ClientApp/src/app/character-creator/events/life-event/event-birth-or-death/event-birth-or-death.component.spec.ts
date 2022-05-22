import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventBirthOrDeathComponent } from './event-birth-or-death.component';

describe('EventBirthOrDeathComponent', () => {
  let component: EventBirthOrDeathComponent;
  let fixture: ComponentFixture<EventBirthOrDeathComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventBirthOrDeathComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventBirthOrDeathComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
