import { TestBed } from '@angular/core/testing';

import { DmService } from './dm.service';

describe('DmsService', () => {
  let service: DmService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DmService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
