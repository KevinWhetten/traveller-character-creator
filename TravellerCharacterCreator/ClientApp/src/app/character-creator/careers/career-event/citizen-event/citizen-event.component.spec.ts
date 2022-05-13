import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CitizenEventComponent } from './citizen-event.component';

describe('CitizenEventComponent', () => {
  let component: CitizenEventComponent;
  let fixture: ComponentFixture<CitizenEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CitizenEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CitizenEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
