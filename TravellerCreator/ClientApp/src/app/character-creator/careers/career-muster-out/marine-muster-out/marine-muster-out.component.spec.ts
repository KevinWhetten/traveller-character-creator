import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MarineMusterOutComponent } from './marine-muster-out.component';

describe('MarineMusterOutComponent', () => {
  let component: MarineMusterOutComponent;
  let fixture: ComponentFixture<MarineMusterOutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MarineMusterOutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MarineMusterOutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
