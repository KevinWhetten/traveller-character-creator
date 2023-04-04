import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CitizenMishapComponent } from './citizen-mishap.component';

describe('CitizenMishapComponent', () => {
  let component: CitizenMishapComponent;
  let fixture: ComponentFixture<CitizenMishapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CitizenMishapComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CitizenMishapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
