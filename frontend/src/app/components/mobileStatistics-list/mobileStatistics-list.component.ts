import { Component, OnDestroy, OnInit } from '@angular/core';
import { MobileStatistics } from 'src/app/models/mobileStatistics.model';
import { MobileStatisticsEvents } from 'src/app/models/mobileStatisticsEvents.model';
import { MobileStatisticsService } from 'src/app/services/mobileStatistics.service';
import { MobileStatisticsWithEventsService } from 'src/app/services/mobileStatisticsWithEvents.service';
import { MobileStatisticsSignalRService } from 'src/app/services/mobileStatisticsSignalR.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-mobileStatistics-list',
  templateUrl: './mobileStatistics-list.component.html',
  styleUrls: ['./mobileStatistics-list.component.css']
})
export class MobileStatisticsListComponent implements OnInit, OnDestroy {
  mobileStatistics?: MobileStatistics[];
  currentMobileStatistics: MobileStatistics = {};
  mobileStatisticsEvents?: MobileStatisticsEvents[];
  currentIndex = -1;
  title = '';
  dataListener?:MobileStatisticsEvents[]
  isCheckBox=true;

  private intervalSub!: Subscription;

  constructor(private mobileStatisticsService: MobileStatisticsService,
    private mobileStatisticsWithEventsService: MobileStatisticsWithEventsService,
    private mobileStatisticsSignalRService: MobileStatisticsSignalRService
  ) { }

  ngOnInit(): void {
    this.routerOnActivate();
    this.mobileStatisticsSignalRService.startConnection();
    this.mobileStatisticsSignalRService.addTransferChartDataListener();
  }
  setActiveMobileStatistics(mobileStatisticsItem: MobileStatistics, mobileStatisticsEventsItem: MobileStatisticsEvents[], index: number): void {
    this.currentMobileStatistics = mobileStatisticsItem;
    this.mobileStatisticsEvents = mobileStatisticsEventsItem;
    this.currentIndex = index;
  }
  routerOnActivate(): void {
    this.intervalSub = this.mobileStatisticsService.getAllPolling()
      .subscribe({
        next: (data) => {
          this.mobileStatistics = data;
        },
        error: (e) => console.error(e)
      });
  }
  
  routerOnDeactivate():void{
    this.intervalSub.unsubscribe();
  }
  checkValue(event: any)
  {
    if(event.target.checked)
    {
      this.routerOnActivate();
    }
    else
    {
      this.routerOnDeactivate();
    }
  }
  getMobileStatisticsEventsById(id: string): void {
    this.mobileStatisticsWithEventsService.get(id)
      .subscribe({
        next: (data) => {
          this.mobileStatisticsEvents = data.events;
        },
        error: (e) => console.error(e)
      });
  }
  ngOnDestroy() {
    this.routerOnDeactivate();
 }
}