import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeverelyInjuredMishapComponent } from './severely-injured-mishap.component';

describe('SeverelyInjuredMishapComponent', () => {
  let component: SeverelyInjuredMishapComponent;
  let fixture: ComponentFixture<SeverelyInjuredMishapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SeverelyInjuredMishapComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SeverelyInjuredMishapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
