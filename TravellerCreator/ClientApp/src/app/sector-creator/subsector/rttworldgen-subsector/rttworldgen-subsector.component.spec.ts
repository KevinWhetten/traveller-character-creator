import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RTTWorldgenSubsectorComponent } from './rttworldgen-subsector.component';

describe('RTTWorldgenSubsectorComponent', () => {
  let component: RTTWorldgenSubsectorComponent;
  let fixture: ComponentFixture<RTTWorldgenSubsectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RTTWorldgenSubsectorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RTTWorldgenSubsectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
