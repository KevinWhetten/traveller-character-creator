import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnusualEventPsionicsComponent } from './unusual-event-psionics.component';

describe('UnusualEventPsionicsComponent', () => {
  let component: UnusualEventPsionicsComponent;
  let fixture: ComponentFixture<UnusualEventPsionicsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnusualEventPsionicsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UnusualEventPsionicsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
