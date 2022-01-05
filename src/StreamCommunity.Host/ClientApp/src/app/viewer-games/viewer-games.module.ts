import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule as AngularCommonModule } from '@angular/common';
import { EnlistmentsPageComponent } from './pages/enlistments-page/enlistments-page.component';
import { OverlaysPageComponent } from './pages/overlays-page/overlays-page.component';
import { CommonModule } from '../common/common.module';
import { Routes, RouterModule } from '@angular/router';
import { ViewerGamesPageComponent } from './pages/viewer-games-page/viewer-games-page.component';

const routes: Routes = [
  { path: 'viewer-games', component: ViewerGamesPageComponent },
  { path: 'viewer-games/enlistments', component: EnlistmentsPageComponent },
];

@NgModule({
  declarations: [
    EnlistmentsPageComponent,
    OverlaysPageComponent,
    ViewerGamesPageComponent,
  ],
  imports: [
    AngularCommonModule,
    HttpClientModule,
    CommonModule,
    RouterModule.forChild(routes),
  ],
})
export class ViewerGamesModule {}
