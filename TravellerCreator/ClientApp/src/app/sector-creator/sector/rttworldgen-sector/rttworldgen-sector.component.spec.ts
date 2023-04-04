import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RTTWorldgenSectorComponent } from './rttworldgen-sector.component';

describe('RTTWorldgenSectorComponent', () => {
  let component: RTTWorldgenSectorComponent;
  let fixture: ComponentFixture<RTTWorldgenSectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RTTWorldgenSectorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RTTWorldgenSectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
