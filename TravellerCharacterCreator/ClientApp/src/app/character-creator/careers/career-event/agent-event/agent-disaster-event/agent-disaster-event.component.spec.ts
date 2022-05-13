import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentDisasterEventComponent } from './agent-disaster-event.component';

describe('AgentDisasterEventComponent', () => {
  let component: AgentDisasterEventComponent;
  let fixture: ComponentFixture<AgentDisasterEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentDisasterEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentDisasterEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
