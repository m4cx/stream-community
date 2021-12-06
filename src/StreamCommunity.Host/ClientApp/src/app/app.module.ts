import { OverlaysPageComponent } from './viewer-games/pages/overlays-page/overlays-page.component';
import { ViewerGamesModule } from './viewer-games/viewer-games.module';
import { EnlistmentsPageComponent } from './viewer-games/pages/enlistments-page/enlistments-page.component';
import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { CardComponent } from './home/components/card/card.component';
import { SignalrService } from './common/signalr.service';
import { CommonModule } from './common/common.module';

@NgModule({
  declarations: [AppComponent, HomeComponent, CardComponent],
  imports: [
    ViewerGamesModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CommonModule,
    RouterModule.forRoot(
      [
        { path: '', component: HomeComponent, pathMatch: 'full' },
        { path: 'enlistments', component: EnlistmentsPageComponent },
        { path: 'overlay', component: OverlaysPageComponent },
      ],
      { relativeLinkResolution: 'legacy' }
    ),
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: (signalrService: SignalrService) => () =>
        signalrService.initiateSignalrConnection(),
      deps: [SignalrService],
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
