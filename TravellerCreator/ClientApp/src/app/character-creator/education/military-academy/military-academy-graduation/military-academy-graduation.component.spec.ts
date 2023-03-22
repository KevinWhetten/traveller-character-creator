import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MilitaryAcademyGraduationComponent } from './military-academy-graduation.component';

describe('MilitaryAcademyGraduationComponent', () => {
  let component: MilitaryAcademyGraduationComponent;
  let fixture: ComponentFixture<MilitaryAcademyGraduationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MilitaryAcademyGraduationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MilitaryAcademyGraduationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
