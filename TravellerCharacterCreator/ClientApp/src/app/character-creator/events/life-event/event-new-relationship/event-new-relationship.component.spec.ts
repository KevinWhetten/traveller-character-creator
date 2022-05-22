import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventNewRelationshipComponent } from './event-new-relationship.component';

describe('EventNewRelationshipComponent', () => {
  let component: EventNewRelationshipComponent;
  let fixture: ComponentFixture<EventNewRelationshipComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventNewRelationshipComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventNewRelationshipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
