import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasicSubsectorComponent } from './basic-subsector.component';

describe('SubsectorComponent', () => {
  let component: BasicSubsectorComponent;
  let fixture: ComponentFixture<BasicSubsectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BasicSubsectorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BasicSubsectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
