import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MongooseSectorComponent } from './mongoose-sector.component';

describe('MongooseSectorComponent', () => {
  let component: MongooseSectorComponent;
  let fixture: ComponentFixture<MongooseSectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MongooseSectorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MongooseSectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
