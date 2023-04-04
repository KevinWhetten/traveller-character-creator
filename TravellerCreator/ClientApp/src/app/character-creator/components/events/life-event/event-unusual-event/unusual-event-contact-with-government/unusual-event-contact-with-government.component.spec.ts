import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnusualEventContactWithGovernmentComponent } from './unusual-event-contact-with-government.component';

describe('UnusualEventContactWithGovernmentComponent', () => {
  let component: UnusualEventContactWithGovernmentComponent;
  let fixture: ComponentFixture<UnusualEventContactWithGovernmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnusualEventContactWithGovernmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UnusualEventContactWithGovernmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
