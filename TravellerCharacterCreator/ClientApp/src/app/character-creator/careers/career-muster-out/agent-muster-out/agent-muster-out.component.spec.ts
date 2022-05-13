import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentMusterOutComponent } from './agent-muster-out.component';

describe('AgentMusterOutComponent', () => {
  let component: AgentMusterOutComponent;
  let fixture: ComponentFixture<AgentMusterOutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentMusterOutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AgentMusterOutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
