import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentConspiracyEventComponent } from './agent-conspiracy-event.component';

describe('AgentConspiracyEventComponent', () => {
  let component: AgentConspiracyEventComponent;
  let fixture: ComponentFixture<AgentConspiracyEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentConspiracyEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentConspiracyEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
