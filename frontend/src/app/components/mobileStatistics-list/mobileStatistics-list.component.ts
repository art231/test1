import { Component, OnDestroy, OnInit } from '@angular/core';
import { MobileStatistics } from 'src/app/models/mobileStatistics.model';
import { MobileStatisticsEvents } from 'src/app/models/mobileStatisticsEvents.model';
import { MobileStatisticsService } from 'src/app/services/mobileStatistics.service';
import { MobileStatisticsWithEventsService } from 'src/app/services/mobileStatisticsWithEvents.service';
import { MobileStatisticsSignalRService } from 'src/app/services/mobileStatisticsSignalR.service';
import { HttpClient } from '@angular/common/http';

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
  timerInterval:any;

  constructor(private mobileStatisticsService: MobileStatisticsService,
    private mobileStatisticsWithEventsService: MobileStatisticsWithEventsService,
    private mobileStatisticsSignalRService: MobileStatisticsSignalRService,
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    this.timerInterval = setInterval(()=>{
      this.retrieveMobileStatistics();
              }, 1000);
    this.mobileStatisticsSignalRService.startConnection();
    this.mobileStatisticsSignalRService.addTransferChartDataListener();
  }
  setActiveMobileStatistics(mobileStatisticsItem: MobileStatistics, mobileStatisticsEventsItem: MobileStatisticsEvents[], index: number): void {
    this.currentMobileStatistics = mobileStatisticsItem;
    this.mobileStatisticsEvents = mobileStatisticsEventsItem;
    this.currentIndex = index;
  }
  retrieveMobileStatistics(): void {
    this.mobileStatisticsService.getAll()
      .subscribe({
        next: (data) => {
          this.mobileStatistics = data;
        },
        error: (e) => console.error(e)
      });
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
          console.log("evet",this.currentMobileStatistics);
        },
        error: (e) => console.error(e)
      });
  }
  routerOnActivate() {
    this.timerInterval = setInterval(()=>{
      this.retrieveMobileStatistics();
              }, 1000);
  }
  
  routerOnDeactivate() {
    clearInterval(this.timerInterval);
  }
  ngOnDestroy() {
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
    }
 }
}