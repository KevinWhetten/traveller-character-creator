import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MilitaryAcademyComponent } from './military-academy.component';

describe('MilitaryAcademyComponent', () => {
  let component: MilitaryAcademyComponent;
  let fixture: ComponentFixture<MilitaryAcademyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MilitaryAcademyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MilitaryAcademyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
