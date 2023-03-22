import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InjurySeverelyInjuredComponent } from './injury-severely-injured.component';

describe('InjurySeverelyInjuredComponent', () => {
  let component: InjurySeverelyInjuredComponent;
  let fixture: ComponentFixture<InjurySeverelyInjuredComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InjurySeverelyInjuredComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InjurySeverelyInjuredComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
