import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RttWorldgenSubsectorComponent } from './rttworldgen-subsector.component';

describe('RttWorldgenSubsectorComponent', () => {
  let component: RttWorldgenSubsectorComponent;
  let fixture: ComponentFixture<RttWorldgenSubsectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RttWorldgenSubsectorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RttWorldgenSubsectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
