import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { switchMap, tap } from 'rxjs/operators';
import { Enlistment } from './enlistment.model';
import { EnlistmentsStore } from './enlistments.store';

@Injectable({ providedIn: 'root' })
export class EnlistmentsService {
  private readonly baseUri = '/api/viewer-games/enlistments';

  constructor(
    protected store: EnlistmentsStore,
    private httpClient: HttpClient
  ) { }

  getEnlistments(): Observable<Enlistment[]> {
    return this.httpClient.get<Enlistment[]>(this.baseUri).pipe(
      tap((x) => {
        const mapped = x.map((value, index) => {
          value.timestamp = new Date(value.timestamp);
          return value;
        })
        .sort((a, b) => a.sortingNo - b.sortingNo);
        this.store.set(mapped);
      })
    );
  }

  draw(enlistment: Enlistment) {
    return this.httpClient
      .put(`${this.baseUri}/${enlistment.id}/draw`, null)
      .pipe(switchMap((x) => this.getEnlistments()));
  }

  close(enlistment: Enlistment) {
    return this.httpClient
      .put(`${this.baseUri}/${enlistment.id}/close`, null)
      .pipe(switchMap((x) => this.getEnlistments()));
  }

  moveUp(enlistment: Enlistment) {
    return this.httpClient
      .put(`${this.baseUri}/${enlistment.id}/move-up`, null)
      .pipe(switchMap(x => this.getEnlistments()));
  }

  moveDown(enlistment: Enlistment) {
    return this.httpClient
      .put(`${this.baseUri}/${enlistment.id}/move-down`, null)
      .pipe(switchMap(x => this.getEnlistments()));
  }
}
