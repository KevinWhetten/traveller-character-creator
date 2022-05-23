import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentSpecialistEventComponent } from './agent-specialist-event.component';

describe('AgentSpecialistEventComponent', () => {
  let component: AgentSpecialistEventComponent;
  let fixture: ComponentFixture<AgentSpecialistEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentSpecialistEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentSpecialistEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
