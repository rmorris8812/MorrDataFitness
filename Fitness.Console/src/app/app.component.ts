import { Component, ViewChild } from '@angular/core';
import { MatMenuTrigger } from '@angular/material';

import { MorrDataService } from './services/morr.data.service';
import { RouteService } from './route.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [ RouteService, MorrDataService ]
})
export class AppComponent {
	@ViewChild(MatMenuTrigger, { static: false }) trigger: MatMenuTrigger;
  	title = 'MorrData';

  	recipes() {
    	this.trigger.openMenu();
  	}
  	food() {
    	this.trigger.openMenu();
  	}
}
