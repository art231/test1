import { Component, Input, OnInit } from '@angular/core';
import { MobileStatisticsWithEventsService } from 'src/app/services/mobileStatisticsWithEvents.service';
import { ActivatedRoute } from '@angular/router';
import { MobileStatisticsEvents } from 'src/app/models/mobileStatisticsEvents.model';

@Component({
  selector: 'app-mobileStatistics-update',
  templateUrl: './mobileStatistics-update.component.html',
  styleUrls: ['./mobileStatistics-update.component.css']
})
export class MobileStatisticsUpdateComponent implements OnInit {

  @Input() viewMode = false;

  @Input() currentMobileStatisticsEvent: MobileStatisticsEvents = { };
  
  message = '';

  constructor(
    private mobileStatisticsWithEventsService: MobileStatisticsWithEventsService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      this.getEvent(this.route.snapshot.params["id"]);
    }
  }

  getEvent(id: string): void {
    this.mobileStatisticsWithEventsService.getEventById(id)
      .subscribe({
        next: (data) => {
          this.currentMobileStatisticsEvent = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  updateMobileStatisticsEvent(): void {
    const data = {
      id:this.currentMobileStatisticsEvent.id,
      lastStatistics: this.currentMobileStatisticsEvent.mobileStatisticsId,
      date:this.currentMobileStatisticsEvent.date,
      name:this.currentMobileStatisticsEvent.name,
      description:this.currentMobileStatisticsEvent.description
    };

    this.message = '';

    this.mobileStatisticsWithEventsService.update(this.currentMobileStatisticsEvent.id, data)
      .subscribe({
        next: (res) => {
          this.message = res.message ? res.message : 'This mobile statistics was updated successfully!';
        },
        error: (e) => console.error(e)
      });
  }
  deleteEvent(id: string): void {
    this.mobileStatisticsWithEventsService.deleteEventById(id)
      .subscribe({
        next: (data) => {
          this.currentMobileStatisticsEvent = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }
}