import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentHomeInjuryComponent } from './agent-home-injury.component';

describe('AgentHomeInjuryComponent', () => {
  let component: AgentHomeInjuryComponent;
  let fixture: ComponentFixture<AgentHomeInjuryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentHomeInjuryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentHomeInjuryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
