import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentLearnMishapComponent } from './agent-learn-mishap.component';

describe('AgentLearnMishapComponent', () => {
  let component: AgentLearnMishapComponent;
  let fixture: ComponentFixture<AgentLearnMishapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentLearnMishapComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentLearnMishapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
