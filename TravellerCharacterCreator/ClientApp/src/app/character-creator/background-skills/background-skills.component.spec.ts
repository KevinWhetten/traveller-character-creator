import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BackgroundSkillsComponent } from './background-skills.component';

describe('BackgroundSkillsComponent', () => {
  let component: BackgroundSkillsComponent;
  let fixture: ComponentFixture<BackgroundSkillsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BackgroundSkillsComponent ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BackgroundSkillsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
