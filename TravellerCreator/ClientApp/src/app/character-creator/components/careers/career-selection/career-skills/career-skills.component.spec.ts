import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerSkillsComponent } from './career-skills.component';

describe('CareerSkillsComponent', () => {
  let component: CareerSkillsComponent;
  let fixture: ComponentFixture<CareerSkillsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerSkillsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerSkillsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
