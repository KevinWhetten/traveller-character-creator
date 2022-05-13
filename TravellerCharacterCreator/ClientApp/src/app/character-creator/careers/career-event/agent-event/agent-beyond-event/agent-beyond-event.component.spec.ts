import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentBeyondEventComponent } from './agent-beyond-event.component';

describe('AgentBeyondEventComponent', () => {
  let component: AgentBeyondEventComponent;
  let fixture: ComponentFixture<AgentBeyondEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentBeyondEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentBeyondEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
