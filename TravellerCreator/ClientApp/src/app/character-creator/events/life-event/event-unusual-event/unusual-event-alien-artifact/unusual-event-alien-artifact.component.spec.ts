import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnusualEventAlienArtifactComponent } from './unusual-event-alien-artifact.component';

describe('UnusualEventAlienArtifactComponent', () => {
  let component: UnusualEventAlienArtifactComponent;
  let fixture: ComponentFixture<UnusualEventAlienArtifactComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnusualEventAlienArtifactComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UnusualEventAlienArtifactComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
