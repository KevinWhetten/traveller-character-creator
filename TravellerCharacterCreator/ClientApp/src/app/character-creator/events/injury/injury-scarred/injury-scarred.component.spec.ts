import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InjuryScarredComponent } from './injury-scarred.component';

describe('InjuryScarredComponent', () => {
  let component: InjuryScarredComponent;
  let fixture: ComponentFixture<InjuryScarredComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InjuryScarredComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InjuryScarredComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
