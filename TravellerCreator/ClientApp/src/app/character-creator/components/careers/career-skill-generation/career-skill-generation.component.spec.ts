import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareerSkillGenerationComponent } from './career-skill-generation.component';

describe('CareerSkillGenerationComponent', () => {
  let component: CareerSkillGenerationComponent;
  let fixture: ComponentFixture<CareerSkillGenerationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareerSkillGenerationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareerSkillGenerationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
