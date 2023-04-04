import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InjuryTableComponent } from './injury-table.component';

describe('InjuryTableComponent', () => {
  let component: InjuryTableComponent;
  let fixture: ComponentFixture<InjuryTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InjuryTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InjuryTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
