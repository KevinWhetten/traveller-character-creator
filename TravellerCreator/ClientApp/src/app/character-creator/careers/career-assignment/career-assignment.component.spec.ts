import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerAssignmentComponent } from './career-assignment.component';

describe('CareerAssignmentComponent', () => {
  let component: CareerAssignmentComponent;
  let fixture: ComponentFixture<CareerAssignmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerAssignmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerAssignmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
