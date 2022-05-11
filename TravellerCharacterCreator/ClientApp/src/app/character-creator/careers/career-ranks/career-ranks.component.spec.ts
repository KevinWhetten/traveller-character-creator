import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerRanksComponent } from './career-ranks.component';

describe('CareerRanksComponent', () => {
  let component: CareerRanksComponent;
  let fixture: ComponentFixture<CareerRanksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerRanksComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerRanksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
