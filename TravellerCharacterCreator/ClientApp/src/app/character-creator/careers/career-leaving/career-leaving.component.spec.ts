import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerLeavingComponent } from './career-leaving.component';

describe('CareerLeavingComponent', () => {
  let component: CareerLeavingComponent;
  let fixture: ComponentFixture<CareerLeavingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerLeavingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerLeavingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
