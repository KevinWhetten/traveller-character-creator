import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerCommissionComponent } from './career-commission.component';

describe('CareerCommissionComponent', () => {
  let component: CareerCommissionComponent;
  let fixture: ComponentFixture<CareerCommissionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerCommissionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerCommissionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
