import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerMusterOutComponent } from './career-muster-out.component';

describe('CareerBenefitsComponent', () => {
  let component: CareerMusterOutComponent;
  let fixture: ComponentFixture<CareerMusterOutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerMusterOutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerMusterOutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
