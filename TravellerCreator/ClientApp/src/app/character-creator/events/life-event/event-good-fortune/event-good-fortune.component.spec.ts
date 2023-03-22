import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventGoodFortuneComponent } from './event-good-fortune.component';

describe('EventGoodFortuneComponent', () => {
  let component: EventGoodFortuneComponent;
  let fixture: ComponentFixture<EventGoodFortuneComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventGoodFortuneComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventGoodFortuneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
