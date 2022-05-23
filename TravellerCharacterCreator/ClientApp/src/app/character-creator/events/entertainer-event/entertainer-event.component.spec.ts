import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EntertainerEventComponent } from './entertainer-event.component';

describe('EntertainerEventComponent', () => {
  let component: EntertainerEventComponent;
  let fixture: ComponentFixture<EntertainerEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EntertainerEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EntertainerEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
