import { TestBed } from '@angular/core/testing';

import { MilitaryAcademyService } from './military-academy.service';

describe('MilitaryAcademyService', () => {
  let service: MilitaryAcademyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MilitaryAcademyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
