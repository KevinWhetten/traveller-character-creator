import { TestBed } from '@angular/core/testing';

import { RollingService } from './rolling.service';

describe('DmsService', () => {
  let service: RollingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RollingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
