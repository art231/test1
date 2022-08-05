import { Component, Input, OnInit } from '@angular/core';
import { MobileStatisticsService } from 'src/app/services/mobileStatistics.service';
import { ActivatedRoute } from '@angular/router';
import { MobileStatistics } from 'src/app/models/mobileStatistics.model';

@Component({
  selector: 'app-mobileStatistics-update',
  templateUrl: './mobileStatistics-update.component.html',
  styleUrls: ['./mobileStatistics-update.component.css']
})
export class MobileStatisticsUpdateComponent implements OnInit {

  @Input() viewMode = false;

  @Input() currentMobileStatistics: MobileStatistics = { };
  
  message = '';

  constructor(
    private mobileStatisticsService: MobileStatisticsService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      this.getMobileStatistics(this.route.snapshot.params["id"]);
    }
  }

  getMobileStatistics(id: string): void {
    this.mobileStatisticsService.get(id)
      .subscribe({
        next: (data) => {
          this.currentMobileStatistics = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  updateMobileStatistics(): void {
    const data = {
      id:this.currentMobileStatistics.id,
      title: this.currentMobileStatistics.title,
      lastStatistics: this.currentMobileStatistics.lastStatistics,
      versionClient:this.currentMobileStatistics.versionClient,
      type:this.currentMobileStatistics.type
    };

    this.message = '';

    this.mobileStatisticsService.update(this.currentMobileStatistics.id, data)
      .subscribe({
        next: (res) => {
          console.log(res);
          this.message = res.message ? res.message : 'This mobile statistics was updated successfully!';
        },
        error: (e) => console.error(e)
      });
  }
}