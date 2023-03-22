import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EducationEventComponent } from './education-event.component';

describe('EducationEventComponent', () => {
  let component: EducationEventComponent;
  let fixture: ComponentFixture<EducationEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EducationEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EducationEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
