import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrisonerEventComponent } from './prisoner-event.component';

describe('PrisonerEventComponent', () => {
  let component: PrisonerEventComponent;
  let fixture: ComponentFixture<PrisonerEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrisonerEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PrisonerEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
