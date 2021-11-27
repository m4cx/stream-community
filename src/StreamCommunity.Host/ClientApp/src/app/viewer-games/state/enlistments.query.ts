import { Injectable } from '@angular/core';
import { QueryEntity } from '@datorama/akita';
import { EnlistmentsStore, EnlistmentsState } from './enlistments.store';

@Injectable({ providedIn: 'root' })
export class EnlistmentsQuery extends QueryEntity<EnlistmentsState> {

  constructor(protected store: EnlistmentsStore) {
    super(store);
  }

}
