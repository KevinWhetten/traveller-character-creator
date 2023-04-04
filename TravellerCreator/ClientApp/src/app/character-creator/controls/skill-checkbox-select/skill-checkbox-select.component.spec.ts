import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SkillCheckboxSelectComponent } from './skill-checkbox-select.component';

describe('SkillCheckboxSelectComponent', () => {
  let component: SkillCheckboxSelectComponent;
  let fixture: ComponentFixture<SkillCheckboxSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SkillCheckboxSelectComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SkillCheckboxSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
