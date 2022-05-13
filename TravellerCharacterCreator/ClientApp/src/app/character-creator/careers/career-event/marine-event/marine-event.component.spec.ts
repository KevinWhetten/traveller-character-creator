import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MarineEventComponent } from './marine-event.component';

describe('MarineEventComponent', () => {
  let component: MarineEventComponent;
  let fixture: ComponentFixture<MarineEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MarineEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MarineEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
