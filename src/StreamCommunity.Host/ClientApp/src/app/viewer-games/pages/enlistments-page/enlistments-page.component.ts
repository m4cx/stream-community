import { EnlistmentsQuery } from './../../state/enlistments.query';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { EnlistmentsService } from '../../state/enlistments.service';
import { Enlistment } from '../../state/enlistment.model';

@Component({
  selector: 'app-enlistments-page',
  templateUrl: './enlistments-page.component.html',
  styleUrls: ['./enlistments-page.component.css'],
})
export class EnlistmentsPageComponent implements OnInit {
  public enlistments$: Observable<Enlistment[]>;

  constructor(
    private enlistmentService: EnlistmentsService,
    private enlistmentQuery: EnlistmentsQuery
  ) {}

  ngOnInit() {
    this.enlistments$ = this.enlistmentQuery.selectAll();
    this.enlistmentService.getEnlistments().toPromise();
  }

  async draw(enlistment: Enlistment) {
    this.enlistmentService.draw(enlistment).toPromise();
  }

  async close(enlistment: Enlistment) {
    this.enlistmentService.close(enlistment).toPromise();
  }

  async reload() {
    await this.enlistmentService.getEnlistments().toPromise();
  }
}
