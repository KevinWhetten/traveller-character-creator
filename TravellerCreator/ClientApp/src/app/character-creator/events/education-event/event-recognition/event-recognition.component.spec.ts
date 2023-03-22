import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventRecognitionComponent } from './event-recognition.component';

describe('EventRecognitionComponent', () => {
  let component: EventRecognitionComponent;
  let fixture: ComponentFixture<EventRecognitionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventRecognitionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventRecognitionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
