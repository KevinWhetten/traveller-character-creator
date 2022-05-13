import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DrifterEventComponent } from './drifter-event.component';

describe('DrifterEventComponent', () => {
  let component: DrifterEventComponent;
  let fixture: ComponentFixture<DrifterEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DrifterEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DrifterEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
