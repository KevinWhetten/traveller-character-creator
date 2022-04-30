import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinalStepsComponent } from './final-steps.component';

describe('FinalStepsComponent', () => {
  let component: FinalStepsComponent;
  let fixture: ComponentFixture<FinalStepsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FinalStepsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FinalStepsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
