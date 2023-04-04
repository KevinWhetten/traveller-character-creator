import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StarFrontiersSubsectorComponent } from './star-frontiers-subsector.component';

describe('StarFrontiersSubsectorComponent', () => {
  let component: StarFrontiersSubsectorComponent;
  let fixture: ComponentFixture<StarFrontiersSubsectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StarFrontiersSubsectorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StarFrontiersSubsectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
