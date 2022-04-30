import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MilitaryAcademyEventComponent } from './military-academy-event.component';

describe('MilitaryAcademyEventComponent', () => {
  let component: MilitaryAcademyEventComponent;
  let fixture: ComponentFixture<MilitaryAcademyEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MilitaryAcademyEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MilitaryAcademyEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
