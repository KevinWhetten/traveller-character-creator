import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RogueEventComponent } from './rogue-event.component';

describe('RogueEventComponent', () => {
  let component: RogueEventComponent;
  let fixture: ComponentFixture<RogueEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RogueEventComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RogueEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
