import { TestBed } from '@angular/core/testing';

import { EnlistmentsService } from './enlistments.service';

describe('EnlistmentsService', () => {
  let service: EnlistmentsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EnlistmentsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
