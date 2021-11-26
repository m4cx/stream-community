import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Enlistment } from '../models/enlistment';

@Injectable({
  providedIn: 'root',
})
export class EnlistmentsService {
  private readonly baseUri = '/api/viewer-games/enlistments';

  constructor(private httpClient: HttpClient) {}

  getEnlistments(): Observable<Enlistment[]> {
    return this.httpClient.get<Enlistment[]>(this.baseUri);
  }

  async draw(enlistment: Enlistment) {
    await this.httpClient
      .put(`${this.baseUri}/${enlistment.id}/draw`, null)
      .toPromise();
  }

  async close(enlistment: Enlistment) {
    await this.httpClient
      .put(`${this.baseUri}/${enlistment.id}/close`, null)
      .toPromise();
  }
}
