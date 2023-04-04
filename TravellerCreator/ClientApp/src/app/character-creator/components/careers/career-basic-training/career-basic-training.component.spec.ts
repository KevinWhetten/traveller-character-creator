import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerBasicTrainingComponent } from './career-basic-training.component';

describe('CareerBasicTrainingComponent', () => {
  let component: CareerBasicTrainingComponent;
  let fixture: ComponentFixture<CareerBasicTrainingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerBasicTrainingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerBasicTrainingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
