import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MilitaryAcademyPsionicTestComponent } from './military-academy-psionic-test.component';

describe('MilitaryAcademyPsionicTestComponent', () => {
  let component: MilitaryAcademyPsionicTestComponent;
  let fixture: ComponentFixture<MilitaryAcademyPsionicTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MilitaryAcademyPsionicTestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MilitaryAcademyPsionicTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
