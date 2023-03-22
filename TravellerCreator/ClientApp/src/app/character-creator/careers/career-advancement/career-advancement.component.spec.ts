import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerAdvancementComponent } from './career-advancement.component';

describe('CareerAdvancementComponent', () => {
  let component: CareerAdvancementComponent;
  let fixture: ComponentFixture<CareerAdvancementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerAdvancementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerAdvancementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
