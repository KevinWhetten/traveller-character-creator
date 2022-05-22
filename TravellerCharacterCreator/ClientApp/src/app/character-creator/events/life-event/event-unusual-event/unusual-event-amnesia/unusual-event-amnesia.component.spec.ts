import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnusualEventAmnesiaComponent } from './unusual-event-amnesia.component';

describe('UnusualEventAmnesiaComponent', () => {
  let component: UnusualEventAmnesiaComponent;
  let fixture: ComponentFixture<UnusualEventAmnesiaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnusualEventAmnesiaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UnusualEventAmnesiaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
