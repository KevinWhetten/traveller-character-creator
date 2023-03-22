import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentDealMishapComponent } from './agent-deal-mishap.component';

describe('AgentDealMishapComponent', () => {
  let component: AgentDealMishapComponent;
  let fixture: ComponentFixture<AgentDealMishapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentDealMishapComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentDealMishapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
