import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerQualificationComponent } from './career-qualification.component';

describe('CareerQualificationComponent', () => {
  let component: CareerQualificationComponent;
  let fixture: ComponentFixture<CareerQualificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerQualificationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerQualificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
