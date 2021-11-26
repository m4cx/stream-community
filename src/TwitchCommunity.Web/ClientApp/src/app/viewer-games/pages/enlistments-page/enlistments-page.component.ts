import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Enlistment } from '../../models/enlistment';
import { EnlistmentsService } from '../../services/enlistments.service';

@Component({
  selector: 'app-enlistments-page',
  templateUrl: './enlistments-page.component.html',
  styleUrls: ['./enlistments-page.component.css'],
})
export class EnlistmentsPageComponent implements OnInit {
  public enlistments$: Observable<Enlistment[]>;

  constructor(private enlistmentsService: EnlistmentsService) {}

  ngOnInit() {
    this.loadData();
  }

  async draw(enlistment: Enlistment) {
    this.enlistmentsService.draw(enlistment);
    this.loadData();
  }

  async close(enlistment: Enlistment) {
    this.enlistmentsService.close(enlistment);
    this.loadData();
  }

  private loadData() {
    this.enlistments$ = this.enlistmentsService.getEnlistments();
  }
}
