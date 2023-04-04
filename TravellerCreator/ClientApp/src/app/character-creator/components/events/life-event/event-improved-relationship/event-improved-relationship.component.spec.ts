import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventImprovedRelationshipComponent } from './event-improved-relationship.component';

describe('EventImprovedRelationshipComponent', () => {
  let component: EventImprovedRelationshipComponent;
  let fixture: ComponentFixture<EventImprovedRelationshipComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventImprovedRelationshipComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventImprovedRelationshipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
