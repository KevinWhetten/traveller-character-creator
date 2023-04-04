import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StarFrontiersSectorComponent } from './star-frontiers-sector.component';

describe('StarFrontiersSectorComponent', () => {
  let component: StarFrontiersSectorComponent;
  let fixture: ComponentFixture<StarFrontiersSectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StarFrontiersSectorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StarFrontiersSectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
