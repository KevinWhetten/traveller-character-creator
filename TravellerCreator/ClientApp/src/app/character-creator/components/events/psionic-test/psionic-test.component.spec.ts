import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PsionicTestComponent } from './psionic-test.component';

describe('PsionicTestComponent', () => {
  let component: PsionicTestComponent;
  let fixture: ComponentFixture<PsionicTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PsionicTestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PsionicTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
