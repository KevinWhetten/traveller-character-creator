import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentLifeEventComponent } from './agent-life-event.component';

describe('AgentLifeEventComponent', () => {
  let component: AgentLifeEventComponent;
  let fixture: ComponentFixture<AgentLifeEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentLifeEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentLifeEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
