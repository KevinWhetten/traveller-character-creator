import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InjuryMissingEyeOrLimbComponent } from './injury-missing-eye-or-limb.component';

describe('InjuryMissingEyeOrLimbComponent', () => {
  let component: InjuryMissingEyeOrLimbComponent;
  let fixture: ComponentFixture<InjuryMissingEyeOrLimbComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InjuryMissingEyeOrLimbComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InjuryMissingEyeOrLimbComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
