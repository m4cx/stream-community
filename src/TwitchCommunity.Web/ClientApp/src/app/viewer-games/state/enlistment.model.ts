import { EnlistmentState } from '../models/enlistment-state';

export interface Enlistment {
  id: number | string;
  userName: string;
  state: EnlistmentState;
}

export function createEnlistment(params: Partial<Enlistment>) {
  return {} as Enlistment;
}
