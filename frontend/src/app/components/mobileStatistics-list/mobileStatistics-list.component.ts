import { Component, OnInit } from '@angular/core';
import { MobileStatistics } from 'src/app/models/mobileStatistics.model';
import { MobileStatisticsEvents } from 'src/app/models/mobileStatisticsEvents.model';
import { MobileStatisticsService } from 'src/app/services/mobileStatistics.service';
import { MobileStatisticsEventsService } from 'src/app/services/mobileStatisticsEvents.service';

@Component({
  selector: 'app-mobileStatistics-list',
  templateUrl: './mobileStatistics-list.component.html',
  styleUrls: ['./mobileStatistics-list.component.css']
})
export class MobileStatisticsListComponent implements OnInit {
  mobileStatistics?: MobileStatistics[];
  currentMobileStatistics: MobileStatistics = {};
  mobileStatisticsEvents?: MobileStatisticsEvents[];
  currentIndex = -1;
  title = '';

  constructor(private mobileStatisticsService: MobileStatisticsService,
    private mobileStatisticsEventsService: MobileStatisticsEventsService) { }

  ngOnInit(): void {
    this.retrieveMobileStatistics();
    this.getMobileStatisticsEventsById(this.currentMobileStatistics.id);
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
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  getMobileStatisticsEventsById(id: string): void {
    this.mobileStatisticsEventsService.get(id)
      .subscribe({
        next: (data) => {
          this.mobileStatisticsEvents = data;
          this.ngOnInit();
          console.log(this.mobileStatisticsEvents);
        },
        error: (e) => console.error(e)
      });
  }
}