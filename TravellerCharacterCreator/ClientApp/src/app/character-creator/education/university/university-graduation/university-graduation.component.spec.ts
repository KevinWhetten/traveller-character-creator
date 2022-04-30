import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UniversityGraduationComponent } from './university-graduation.component';

describe('UniversityGraduationComponent', () => {
  let component: UniversityGraduationComponent;
  let fixture: ComponentFixture<UniversityGraduationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UniversityGraduationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UniversityGraduationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
