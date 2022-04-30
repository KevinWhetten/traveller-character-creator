import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PsionComponent } from './psion.component';

describe('PsionComponent', () => {
  let component: PsionComponent;
  let fixture: ComponentFixture<PsionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PsionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PsionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
