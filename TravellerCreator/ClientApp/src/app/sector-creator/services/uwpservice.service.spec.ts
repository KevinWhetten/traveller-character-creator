import { TestBed } from '@angular/core/testing';
import {UWPService} from "./uwp.service";

describe('UWPServiceService', () => {
  let service: UWPService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UWPService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
