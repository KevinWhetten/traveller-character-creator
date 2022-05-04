import { TestBed } from '@angular/core/testing';

import { CharacterMetadataService } from './character-metadata.service';

describe('CharacterCreationService', () => {
  let service: CharacterMetadataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CharacterMetadataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
