import { Component, OnInit } from '@angular/core';
import { MobileStatistics } from 'src/app/models/mobileStatistics.model';
import { MobileStatisticsService } from 'src/app/services/mobileStatistics.service';

@Component({
  selector: 'app-mobileStatistics-list',
  templateUrl: './mobileStatistics-list.component.html',
  styleUrls: ['./mobileStatistics-list.component.css']
})
export class MobileStatisticsListComponent implements OnInit {
  mobileStatistics?: MobileStatistics[];
  currentMobileStatistics: MobileStatistics = {};
  currentIndex = -1;
  title = '';

  constructor(private mobileStatisticsService: MobileStatisticsService) { }

  ngOnInit(): void {
    this.retrieveMobileStatistics();
  }
  setActiveMobileStatistics(mobileStatisticsItem: MobileStatistics, index: number): void {
    this.currentMobileStatistics = mobileStatisticsItem;
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
}