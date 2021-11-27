import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit {

  constructor() { }

  @Input()
  public title: string;

  @Input()
  public description: string;

  @Input()
  public route: Route;

  ngOnInit(): void {
  }

}

export class Route {
  constructor(public title:string, public route: Array<string>) {
  }
}
