import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnusualEventAncientTechnologyComponent } from './unusual-event-ancient-technology.component';

describe('UnusualEventAncientTechnologyComponent', () => {
  let component: UnusualEventAncientTechnologyComponent;
  let fixture: ComponentFixture<UnusualEventAncientTechnologyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnusualEventAncientTechnologyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UnusualEventAncientTechnologyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
