import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventSicknessOrInjuryComponent } from './event-sickness-or-injury.component';

describe('EventSicknessOrInjuryComponent', () => {
  let component: EventSicknessOrInjuryComponent;
  let fixture: ComponentFixture<EventSicknessOrInjuryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventSicknessOrInjuryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventSicknessOrInjuryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
