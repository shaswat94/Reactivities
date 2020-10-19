import { Component, OnInit } from '@angular/core';
import { Activity } from 'src/app/models/activity';

import { ActivityService } from 'src/app/services/activity.service';

@Component({
  selector: 'app-activity',
  templateUrl: './activity.component.html',
  styleUrls: ['./activity.component.css']
})
export class ActivityComponent implements OnInit {
  activities: Activity[];
  constructor(private activityService: ActivityService) { }

  ngOnInit() {
    this.getActivities();
  }

  getActivities() : void {
    this.activityService.getActivities().subscribe(activities => {
      this.activities = activities;
      console.log(this.activities);
    });
  }
}
