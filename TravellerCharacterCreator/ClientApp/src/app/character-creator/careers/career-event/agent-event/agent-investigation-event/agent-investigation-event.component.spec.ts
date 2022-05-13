import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentInvestigationEventComponent } from './agent-investigation-event.component';

describe('AgentInvestigationEventComponent', () => {
  let component: AgentInvestigationEventComponent;
  let fixture: ComponentFixture<AgentInvestigationEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentInvestigationEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentInvestigationEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
