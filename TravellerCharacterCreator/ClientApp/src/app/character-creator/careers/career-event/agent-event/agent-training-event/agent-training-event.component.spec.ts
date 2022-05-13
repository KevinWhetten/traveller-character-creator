import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentTrainingEventComponent } from './agent-training-event.component';

describe('AgentTrainingEventComponent', () => {
  let component: AgentTrainingEventComponent;
  let fixture: ComponentFixture<AgentTrainingEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentTrainingEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentTrainingEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
