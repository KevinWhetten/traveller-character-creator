import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CharacteristicRollComponent } from './characteristic-roll.component';

describe('CharacteristicRollComponent', () => {
  let component: CharacteristicRollComponent;
  let fixture: ComponentFixture<CharacteristicRollComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CharacteristicRollComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CharacteristicRollComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
