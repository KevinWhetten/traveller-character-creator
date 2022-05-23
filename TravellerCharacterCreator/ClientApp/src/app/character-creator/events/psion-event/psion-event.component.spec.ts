import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PsionEventComponent } from './psion-event.component';

describe('PsionEventComponent', () => {
  let component: PsionEventComponent;
  let fixture: ComponentFixture<PsionEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PsionEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PsionEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
