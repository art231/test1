import { TestBed } from '@angular/core/testing';

import { MobileStatisticsService } from './mobileStatistics.service';

describe('MobileStatisticsService', () => {
  let service: MobileStatisticsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MobileStatisticsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
