import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UniversitySkillsComponent } from './university-skills.component';

describe('UniversitySkillsComponent', () => {
  let component: UniversitySkillsComponent;
  let fixture: ComponentFixture<UniversitySkillsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UniversitySkillsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UniversitySkillsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
