import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DrifterComponent } from './drifter.component';

describe('DrifterComponent', () => {
  let component: DrifterComponent;
  let fixture: ComponentFixture<DrifterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DrifterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DrifterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
