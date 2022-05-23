import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NobleEventComponent } from './noble-event.component';

describe('NobleEventComponent', () => {
  let component: NobleEventComponent;
  let fixture: ComponentFixture<NobleEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NobleEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NobleEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
