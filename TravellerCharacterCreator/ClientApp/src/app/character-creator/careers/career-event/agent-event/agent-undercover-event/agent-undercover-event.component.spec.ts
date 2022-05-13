import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentUndercoverEventComponent } from './agent-undercover-event.component';

describe('AgentUndercoverEventComponent', () => {
  let component: AgentUndercoverEventComponent;
  let fixture: ComponentFixture<AgentUndercoverEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentUndercoverEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentUndercoverEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
