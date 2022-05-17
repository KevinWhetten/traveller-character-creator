import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentHomeMishapComponent } from './agent-home-mishap.component';

describe('AgentHomeMishapComponent', () => {
  let component: AgentHomeMishapComponent;
  let fixture: ComponentFixture<AgentHomeMishapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentHomeMishapComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentHomeMishapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
