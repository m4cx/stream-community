import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { EnlistmentsPageComponent } from "./pages/enlistments-page/enlistments-page.component";

@NgModule({
  declarations: [EnlistmentsPageComponent],
  imports: [CommonModule, HttpClientModule],
})
export class ViewerGamesModule {}
