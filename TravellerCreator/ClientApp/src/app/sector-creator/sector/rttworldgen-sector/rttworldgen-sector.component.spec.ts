import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RttWorldgenSectorComponent } from './rttworldgen-sector.component';

describe('RttWorldgenSectorComponent', () => {
  let component: RttWorldgenSectorComponent;
  let fixture: ComponentFixture<RttWorldgenSectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RttWorldgenSectorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RttWorldgenSectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
