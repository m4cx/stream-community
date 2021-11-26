import { ViewerGamesModule } from "./viewer-games/viewer-games.module";
import { EnlistmentsPageComponent } from "./viewer-games/pages/enlistments-page/enlistments-page.component";
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { HomeComponent } from "./home/home.component";
import { CardComponent } from './home/components/card/card.component';

@NgModule({
  declarations: [AppComponent, NavMenuComponent, HomeComponent, CardComponent],
  imports: [
    ViewerGamesModule,
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(
      [
        { path: "", component: HomeComponent, pathMatch: "full" },
        { path: "enlistments", component: EnlistmentsPageComponent },
      ],
      { relativeLinkResolution: "legacy" }
    ),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
