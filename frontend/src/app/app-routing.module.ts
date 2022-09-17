import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddMobileStatisticsComponent } from './components/add-mobileStatistics/add-mobileStatistics.component';
import { MobileStatisticsUpdateComponent } from './components/mobileStatistics-update/mobileStatistics-update.component';
import { MobileStatisticsListComponent } from './components/mobileStatistics-list/mobileStatistics-list.component';

const routes: Routes = [
  { path: '', redirectTo: 'mobileStatistics', pathMatch: 'full' },
  { path: 'add', component: AddMobileStatisticsComponent },
  { path: 'mobileStatistics/event/:id', component: MobileStatisticsUpdateComponent },
  { path: 'mobileStatistics', component: MobileStatisticsListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }