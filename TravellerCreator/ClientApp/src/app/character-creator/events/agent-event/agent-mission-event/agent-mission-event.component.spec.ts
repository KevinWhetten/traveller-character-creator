import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentMissionEventComponent } from './agent-mission-event.component';

describe('AgentMissionEventComponent', () => {
  let component: AgentMissionEventComponent;
  let fixture: ComponentFixture<AgentMissionEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentMissionEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentMissionEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
