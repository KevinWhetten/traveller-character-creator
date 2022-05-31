import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InjuryLightlyInjuredComponent } from './injury-lightly-injured.component';

describe('InjuryLightlyInjuredComponent', () => {
  let component: InjuryLightlyInjuredComponent;
  let fixture: ComponentFixture<InjuryLightlyInjuredComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InjuryLightlyInjuredComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InjuryLightlyInjuredComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
