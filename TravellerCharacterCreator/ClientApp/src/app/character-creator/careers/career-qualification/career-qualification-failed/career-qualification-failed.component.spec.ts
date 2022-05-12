import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerQualificationFailedComponent } from './career-qualification-failed.component';

describe('CareerQualificationFailedComponent', () => {
  let component: CareerQualificationFailedComponent;
  let fixture: ComponentFixture<CareerQualificationFailedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerQualificationFailedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerQualificationFailedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
