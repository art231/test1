import { Component, Input, OnInit } from '@angular/core';
import { TutorialService } from 'src/app/services/tutorial.service';
import { ActivatedRoute } from '@angular/router';
import { Tutorial } from 'src/app/models/tutorial.model';

@Component({
  selector: 'app-tutorial-update',
  templateUrl: './tutorial-update.component.html',
  styleUrls: ['./tutorial-update.component.css']
})
export class TutorialUpdateComponent implements OnInit {

  @Input() viewMode = false;

  @Input() currentTutorial: Tutorial = {
    id:'',
    title: '',
    lastStatistics: '',
    versionClient: '',
    type:''
  };
  
  message = '';

  constructor(
    private tutorialService: TutorialService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    if (!this.viewMode) {
      this.message = '';
      this.getTutorial(this.route.snapshot.params["id"]);
    }
  }

  getTutorial(id: string): void {
    this.tutorialService.get(id)
      .subscribe({
        next: (data) => {
          this.currentTutorial = data;
          console.log(data);
        },
        error: (e) => console.error(e)
      });
  }

  updateTutorial(): void {
    const data = {
      id:this.currentTutorial.id,
      title: this.currentTutorial.title,
      lastStatistics: this.currentTutorial.lastStatistics,
      versionClient:this.currentTutorial.versionClient,
      type:this.currentTutorial.type
    };

    this.message = '';

    this.tutorialService.update(this.currentTutorial.id, data)
      .subscribe({
        next: (res) => {
          console.log(res);
          //this.currentTutorial.published = status;
          this.message = res.message ? res.message : 'This tutorial was updated successfully!';
        },
        error: (e) => console.error(e)
      });
  }

  // updateTutorial(): void {
  //   this.message = '';

  //   this.tutorialService.update(this.currentTutorial.id, this.currentTutorial)
  //     .subscribe({
  //       next: (res) => {
  //         console.log(res);
  //         this.message = res.message ? res.message : 'This tutorial was updated successfully!';
  //       },
  //       error: (e) => console.error(e)
  //     });
  // }

}