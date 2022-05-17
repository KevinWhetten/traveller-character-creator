import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InjuredMishapComponent } from './injured-mishap.component';

describe('InjuredMishapComponent', () => {
  let component: InjuredMishapComponent;
  let fixture: ComponentFixture<InjuredMishapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InjuredMishapComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InjuredMishapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
