import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InjuryInjuredComponent } from './injury-injured.component';

describe('InjuryInjuredComponent', () => {
  let component: InjuryInjuredComponent;
  let fixture: ComponentFixture<InjuryInjuredComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InjuryInjuredComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InjuryInjuredComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
