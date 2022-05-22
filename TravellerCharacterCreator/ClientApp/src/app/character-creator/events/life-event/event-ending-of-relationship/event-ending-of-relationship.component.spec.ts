import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventEndingOfRelationshipComponent } from './event-ending-of-relationship.component';

describe('EventEndingOfRelationshipComponent', () => {
  let component: EventEndingOfRelationshipComponent;
  let fixture: ComponentFixture<EventEndingOfRelationshipComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventEndingOfRelationshipComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventEndingOfRelationshipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
