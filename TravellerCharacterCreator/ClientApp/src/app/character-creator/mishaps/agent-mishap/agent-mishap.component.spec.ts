import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentMishapComponent } from './agent-mishap.component';

describe('AgentMishapComponent', () => {
  let component: AgentMishapComponent;
  let fixture: ComponentFixture<AgentMishapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentMishapComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentMishapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
