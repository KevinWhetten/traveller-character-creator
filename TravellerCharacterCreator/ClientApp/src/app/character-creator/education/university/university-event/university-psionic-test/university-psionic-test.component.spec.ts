import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UniversityPsionicTestComponent } from './university-psionic-test.component';

describe('UniversityPsionicTestComponent', () => {
  let component: UniversityPsionicTestComponent;
  let fixture: ComponentFixture<UniversityPsionicTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UniversityPsionicTestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UniversityPsionicTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
