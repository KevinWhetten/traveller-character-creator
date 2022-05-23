import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentSeniorEventComponent } from './agent-senior-event.component';

describe('AgentSeniorEventComponent', () => {
  let component: AgentSeniorEventComponent;
  let fixture: ComponentFixture<AgentSeniorEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentSeniorEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentSeniorEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
