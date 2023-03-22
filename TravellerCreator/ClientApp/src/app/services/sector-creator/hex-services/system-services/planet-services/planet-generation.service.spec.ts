import { TestBed } from '@angular/core/testing';

import { PlanetGenerationService } from './planet-generation.service';

describe('PlanetGenerationService', () => {
  let service: PlanetGenerationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PlanetGenerationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
