import { Component, OnInit, Inject } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Router, ActivatedRoute, ParamMap } from '@angular/router'
import { Observable, Subscriber } from 'rxjs';
import { tap, map, filter } from 'rxjs/operators';

import { FoodDto } from '../services/food.dto';
import { MorrDataService } from '../services/morr.recipes.service';

@Component({
  selector: 'app-food',
  templateUrl: 'food.component.html',
  styles: ['table { width: 100%;}']
})
export class FoodComponent implements OnInit {
  food: FoodDto[] = [];
  displayedColumns: string[] = ['Name', 'Calories', 'Carbs', 'Fat', 'Sodium', 'Fiber'];

  length = 100;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  pageEvent: PageEvent;

  category: string = '';

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
    this.route.params.subscribe(params => {
      this.category = params['id'];
    });
  }
}