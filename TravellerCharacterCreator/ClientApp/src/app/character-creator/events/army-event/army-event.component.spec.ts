import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArmyEventComponent } from './army-event.component';

describe('ArmyEventComponent', () => {
  let component: ArmyEventComponent;
  let fixture: ComponentFixture<ArmyEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArmyEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ArmyEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
