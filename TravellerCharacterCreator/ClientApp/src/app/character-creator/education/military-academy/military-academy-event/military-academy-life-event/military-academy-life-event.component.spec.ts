import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MilitaryAcademyLifeEventComponent } from './military-academy-life-event.component';

describe('MilitaryAcademyLifeEventComponent', () => {
  let component: MilitaryAcademyLifeEventComponent;
  let fixture: ComponentFixture<MilitaryAcademyLifeEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MilitaryAcademyLifeEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MilitaryAcademyLifeEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
