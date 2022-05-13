import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScoutEventComponent } from './scout-event.component';

describe('ScoutEventComponent', () => {
  let component: ScoutEventComponent;
  let fixture: ComponentFixture<ScoutEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ScoutEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ScoutEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
