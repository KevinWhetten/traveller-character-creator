import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentSeverelyInjuredMishapComponent } from './agent-severely-injured-mishap.component';

describe('AgentSeverelyInjuredMishapComponent', () => {
  let component: AgentSeverelyInjuredMishapComponent;
  let fixture: ComponentFixture<AgentSeverelyInjuredMishapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentSeverelyInjuredMishapComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentSeverelyInjuredMishapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
