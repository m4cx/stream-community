import { Injectable } from '@angular/core';
import { EntityState, EntityStore, StoreConfig } from '@datorama/akita';
import { Enlistment } from './enlistment.model';

export interface EnlistmentsState extends EntityState<Enlistment> {}

@Injectable({ providedIn: 'root' })
@StoreConfig({ name: 'enlistments' })
export class EnlistmentsStore extends EntityStore<EnlistmentsState> {
  constructor() {
    super();
  }
}
