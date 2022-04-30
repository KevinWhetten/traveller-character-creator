import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EducationLifeEventComponent } from './education-life-event.component';

describe('EducationLifeEventComponent', () => {
  let component: EducationLifeEventComponent;
  let fixture: ComponentFixture<EducationLifeEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EducationLifeEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EducationLifeEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
