import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FirstCareerBasicTrainingComponent } from './first-career-basic-training.component';

describe('FirstCareerBasicTrainingComponent', () => {
  let component: FirstCareerBasicTrainingComponent;
  let fixture: ComponentFixture<FirstCareerBasicTrainingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FirstCareerBasicTrainingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FirstCareerBasicTrainingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
