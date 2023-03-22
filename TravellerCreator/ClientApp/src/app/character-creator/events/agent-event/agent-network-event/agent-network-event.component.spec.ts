import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentNetworkEventComponent } from './agent-network-event.component';

describe('AgentNetworkEventComponent', () => {
  let component: AgentNetworkEventComponent;
  let fixture: ComponentFixture<AgentNetworkEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentNetworkEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentNetworkEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
