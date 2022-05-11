import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EducationPsionicTestComponent } from './education-psionic-test.component';

describe('EducationPsionicTestComponent', () => {
  let component: EducationPsionicTestComponent;
  let fixture: ComponentFixture<EducationPsionicTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EducationPsionicTestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EducationPsionicTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
