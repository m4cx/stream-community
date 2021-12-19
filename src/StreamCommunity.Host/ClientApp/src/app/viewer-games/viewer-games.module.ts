import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule as AngularCommonModule } from '@angular/common';
import { EnlistmentsPageComponent } from './pages/enlistments-page/enlistments-page.component';
import { OverlaysPageComponent } from './pages/overlays-page/overlays-page.component';
import { CommonModule } from '../common/common.module';

@NgModule({
  declarations: [EnlistmentsPageComponent, OverlaysPageComponent],
  imports: [AngularCommonModule, HttpClientModule, CommonModule],
})
export class ViewerGamesModule {}
