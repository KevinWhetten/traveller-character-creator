import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScholarEventComponent } from './scholar-event.component';

describe('ScholarEventComponent', () => {
  let component: ScholarEventComponent;
  let fixture: ComponentFixture<ScholarEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ScholarEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ScholarEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
