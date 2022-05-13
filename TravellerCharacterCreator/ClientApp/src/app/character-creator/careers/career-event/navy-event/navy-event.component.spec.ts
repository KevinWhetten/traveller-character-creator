import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavyEventComponent } from './navy-event.component';

describe('NavyEventComponent', () => {
  let component: NavyEventComponent;
  let fixture: ComponentFixture<NavyEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NavyEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NavyEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
