import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentInjuredMishapComponent } from './agent-injured-mishap.component';

describe('AgentInjuredMishapComponent', () => {
  let component: AgentInjuredMishapComponent;
  let fixture: ComponentFixture<AgentInjuredMishapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentInjuredMishapComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentInjuredMishapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
