import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UniversityEventComponent } from './university-event.component';

describe('UniversityEventComponent', () => {
  let component: UniversityEventComponent;
  let fixture: ComponentFixture<UniversityEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UniversityEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UniversityEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
