import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetermineCharacteristicsComponent } from './determine-characteristics.component';

describe('CharacteristicsComponent', () => {
  let component: DetermineCharacteristicsComponent;
  let fixture: ComponentFixture<DetermineCharacteristicsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetermineCharacteristicsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DetermineCharacteristicsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
