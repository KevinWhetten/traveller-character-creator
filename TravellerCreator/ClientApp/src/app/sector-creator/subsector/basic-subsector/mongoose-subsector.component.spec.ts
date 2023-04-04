import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MongooseSubsectorComponent } from './mongoose-subsector.component';

describe('SubsectorComponent', () => {
  let component: MongooseSubsectorComponent;
  let fixture: ComponentFixture<MongooseSubsectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MongooseSubsectorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MongooseSubsectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
