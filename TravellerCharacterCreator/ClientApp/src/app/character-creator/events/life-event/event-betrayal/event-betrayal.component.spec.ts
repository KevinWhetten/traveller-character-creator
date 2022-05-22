import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventBetrayalComponent } from './event-betrayal.component';

describe('EventBetrayalComponent', () => {
  let component: EventBetrayalComponent;
  let fixture: ComponentFixture<EventBetrayalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventBetrayalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventBetrayalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
