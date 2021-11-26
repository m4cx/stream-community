import { Component } from '@angular/core';
import {Route} from "./components/card/card.component";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public enlistmentsRoute = new Route("Enlistments", ["enlistments"])
}
