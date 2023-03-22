import { TestBed } from '@angular/core/testing';

import { PlanetDesirabilityService } from './planet-desirability.service';

describe('PlanetDesirabilityService', () => {
  let service: PlanetDesirabilityService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PlanetDesirabilityService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
