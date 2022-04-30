import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UniversityLifeEventComponent } from './university-life-event.component';

describe('UniversityLifeEventComponent', () => {
  let component: UniversityLifeEventComponent;
  let fixture: ComponentFixture<UniversityLifeEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UniversityLifeEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UniversityLifeEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
