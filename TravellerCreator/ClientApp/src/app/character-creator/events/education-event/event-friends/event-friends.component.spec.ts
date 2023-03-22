import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventFriendsComponent } from './event-friends.component';

describe('EventFriendsComponent', () => {
  let component: EventFriendsComponent;
  let fixture: ComponentFixture<EventFriendsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventFriendsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EventFriendsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
