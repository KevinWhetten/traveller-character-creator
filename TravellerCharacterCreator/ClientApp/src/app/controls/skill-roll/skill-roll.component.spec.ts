import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SkillRollComponent } from './skill-roll.component';

describe('SkillRollComponent', () => {
  let component: SkillRollComponent;
  let fixture: ComponentFixture<SkillRollComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SkillRollComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SkillRollComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
