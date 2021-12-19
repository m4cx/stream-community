import { RouterModule } from '@angular/router';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { CommonModule as AngularCommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

@NgModule({
  declarations: [NavMenuComponent],
  exports: [NavMenuComponent],
  imports: [AngularCommonModule, RouterModule],
})
export class CommonModule {}
