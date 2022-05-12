import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerSurvivalComponent } from './career-survival.component';

describe('CareerSurvivalComponent', () => {
  let component: CareerSurvivalComponent;
  let fixture: ComponentFixture<CareerSurvivalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerSurvivalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerSurvivalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
