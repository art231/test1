import { Component } from '@angular/core';
import { MobileStatistics } from 'src/app/models/mobileStatistics.model';
import { MobileStatisticsService } from 'src/app/services/mobileStatistics.service';

@Component({
  selector: 'app-add-mobileStatistics',
  templateUrl: './add-mobileStatistics.component.html',
  styleUrls: ['./add-mobileStatistics.component.css']
})
export class AddMobileStatisticsComponent {

  mobileStatisticsItem: MobileStatistics = {
    id:'',
    title: '',
    lastStatistics: '',
    versionClient: '',
    type:''
  };
  submitted = false;

  constructor(private mobileStatisticsService: MobileStatisticsService) { }

  saveMobileStatistics(): void {
    const data = {
      title: this.mobileStatisticsItem.title,
      lastStatistics: this.mobileStatisticsItem.lastStatistics,
      versionClient: this.mobileStatisticsItem.versionClient,
      type: this.mobileStatisticsItem.type
    };

    this.mobileStatisticsService.create(data)
      .subscribe({
        next: (res) => {
          console.log(res);
          this.submitted = true;
        },
        error: (e) => console.error(e)
      });
  }

  newMobileStatistics(): void {
    this.submitted = false;
    this.mobileStatisticsItem = {
      title: '',
      lastStatistics: '',
      versionClient: '',
      type:''
    };
  }

}