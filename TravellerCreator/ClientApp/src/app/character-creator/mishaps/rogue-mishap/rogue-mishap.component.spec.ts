import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RogueMishapComponent } from './rogue-mishap.component';

describe('RogueMishapComponent', () => {
  let component: RogueMishapComponent;
  let fixture: ComponentFixture<RogueMishapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RogueMishapComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RogueMishapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
