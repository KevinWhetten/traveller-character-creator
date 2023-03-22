import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnusualEventAliensComponent } from './unusual-event-aliens.component';

describe('UnusualEventAliensComponent', () => {
  let component: UnusualEventAliensComponent;
  let fixture: ComponentFixture<UnusualEventAliensComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnusualEventAliensComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UnusualEventAliensComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
