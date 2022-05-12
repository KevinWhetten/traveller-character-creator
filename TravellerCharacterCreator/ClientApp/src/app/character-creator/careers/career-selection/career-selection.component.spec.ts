import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerSelectionComponent } from './career-selection.component';

describe('CareersComponent', () => {
  let component: CareerSelectionComponent;
  let fixture: ComponentFixture<CareerSelectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerSelectionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
