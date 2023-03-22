import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentEventComponent } from './agent-event.component';

describe('AgentEventComponent', () => {
  let component: AgentEventComponent;
  let fixture: ComponentFixture<AgentEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
