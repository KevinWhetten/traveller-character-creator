import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemInfoDialog } from './system-info-component-dialog.component';

describe('SystemInfoComponent', () => {
  let component: SystemInfoDialog;
  let fixture: ComponentFixture<SystemInfoDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SystemInfoDialog ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SystemInfoDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
