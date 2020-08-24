import { Component, OnInit, Inject } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Router, ActivatedRoute, ParamMap } from '@angular/router'
import { Observable, Subscriber } from 'rxjs';
import { tap, map, filter } from 'rxjs/operators';

import { MorrDataService } from '../services/morr.data.service';
import { UserDto } from '../services/user.dto';

@Component({
  selector: 'app-home',
  templateUrl: 'home.component.html',
  styles: ['table { width: 100%;}']
})
export class HomeComponent implements OnInit {
  /**
   * Ctor.
   * @param router - the router service to use to get url parameters.
   * @param lea - the service to use to interface to the ILS Rest API.
   * @param dialog - the dialog to use to generate a key.
   */
	constructor(@Inject(ActivatedRoute) private route: ActivatedRoute,
              @Inject(Router) private router: Router, 
              @Inject(MorrDataService) private service: MorrDataService) {
	}

  ngOnInit() {
  }
}