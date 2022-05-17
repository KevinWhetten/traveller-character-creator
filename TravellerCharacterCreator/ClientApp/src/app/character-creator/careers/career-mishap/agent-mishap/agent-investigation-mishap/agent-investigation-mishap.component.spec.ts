import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentInvestigationMishapComponent } from './agent-investigation-mishap.component';

describe('AgentInvestigationMishapComponent', () => {
  let component: AgentInvestigationMishapComponent;
  let fixture: ComponentFixture<AgentInvestigationMishapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentInvestigationMishapComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentInvestigationMishapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
