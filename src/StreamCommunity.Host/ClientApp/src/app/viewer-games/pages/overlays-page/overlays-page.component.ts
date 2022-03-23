import { EnlistmentsService } from './../../state/enlistments.service';
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { EnlistmentState } from '../../models/enlistment-state';
import { EnlistmentsQuery } from '../../state/enlistments.query';
import { Enlistment } from '../../state/enlistment.model';
import { SignalrService } from 'src/app/common/signalr.service';

@Component({
  selector: 'app-overlays-page',
  templateUrl: './overlays-page.component.html',
  styleUrls: ['./overlays-page.component.css'],
})
export class OverlaysPageComponent implements OnInit {
  constructor(
    private query: EnlistmentsQuery,
    private enlistmentsService: EnlistmentsService,
    private signalrService: SignalrService
  ) {}

  public waiting$: Observable<Enlistment[]>;

  ngOnInit(): void {
    this.enlistmentsService.getEnlistments().toPromise();
    this.waiting$ = this.query.selectAll({
      filterBy: ({ state }) => state === EnlistmentState.Open,
      limitTo: 5,
    });

    this.signalrService.connection.on('notify', (param) => {
      console.log('Received event: ', param);
      if (['PLAYER_ENLISTED', 'PLAYER_DRAWN', 'PLAYER_WITHDRAWN'].includes(param.type)) {
        this.enlistmentsService.getEnlistments().toPromise();
      }
    });
  }

  public duration(x: Enlistment) {
    const now = new Date();
    const diffMs = <any>now - <any>x.timestamp; // milliseconds between now & Christmas
    const diffDays = Math.floor(diffMs / 86400000); // days
    const diffHrs = Math.floor((diffMs % 86400000) / 3600000); // hours
    const diffMins = Math.round(((diffMs % 86400000) % 3600000) / 60000); // minutes

    let text = 'Wartet bereits ';
    if (diffDays > 0) {
      text += `${diffDays} Tage `;
    }
    if (diffHrs > 0) {
      text += `${diffHrs} Stunden `;
    }
    if (diffMins > 0) {
      text += `${diffMins} Minuten `;
    }

    return text;
  }
}
