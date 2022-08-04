import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddMobileStatisticsComponent } from './components/add-mobileStatistics/add-mobileStatistics.component';
import { MobileStatisticsUpdateComponent } from './components/mobileStatistics-update/mobileStatistics-update.component';
import { MobileStatisticsListComponent } from './components/mobileStatistics-list/mobileStatistics-list.component';

@NgModule({
  declarations: [
    AppComponent,
    AddMobileStatisticsComponent,
    MobileStatisticsUpdateComponent,
    MobileStatisticsListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
