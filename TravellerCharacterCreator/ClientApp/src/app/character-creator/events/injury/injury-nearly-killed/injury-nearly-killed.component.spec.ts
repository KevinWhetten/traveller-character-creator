import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InjuryNearlyKilledComponent } from './injury-nearly-killed.component';

describe('InjuryNearlyKilledComponent', () => {
  let component: InjuryNearlyKilledComponent;
  let fixture: ComponentFixture<InjuryNearlyKilledComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InjuryNearlyKilledComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InjuryNearlyKilledComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
