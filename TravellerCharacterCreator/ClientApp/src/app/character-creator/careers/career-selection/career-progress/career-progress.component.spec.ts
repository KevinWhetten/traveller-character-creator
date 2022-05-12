import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerProgressComponent } from './career-progress.component';

describe('CareerProgressComponent', () => {
  let component: CareerProgressComponent;
  let fixture: ComponentFixture<CareerProgressComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerProgressComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerProgressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
