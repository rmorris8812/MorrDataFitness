import { Component, OnInit, Inject } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Router, ActivatedRoute, ParamMap } from '@angular/router'
import { Observable, Subscriber } from 'rxjs';
import { tap, map, filter } from 'rxjs/operators';

import { FoodDto } from '../services/food.dto';
import { MorrDataService } from '../services/morr.recipes.service';

@Component({
  selector: 'app-diet',
  templateUrl: 'diet.component.html',
  styles: ['table { width: 100%;}']
})
export class DietComponent implements OnInit {
  displayedColumns: string[] = ['Name', 'Calories', 'Carbs', 'Fat', 'Sodium', 'Fiber'];

  length = 100;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  pageEvent: PageEvent;

  food: FoodDto[] = [];
  category: string = '';
  date: string = '03.07.2020';
  totals: FoodDto[] = [];

  /**
   * Ctor.
   * @param router - the router service to use to get url parameters.
   * @param lea - the service to use to interface to the ILS Rest API.
   * @param dialog - the dialog to use to generate a key.
   */
	constructor(@Inject(ActivatedRoute) private route: ActivatedRoute,
              @Inject(Router) private router: Router, 
              @Inject(MorrDataService) private service: MorrDataService) {
    var breakfast = new FoodDto();
    breakfast.name = 'Breakfast';
    this.totals.push(breakfast);    
    var lunch = new FoodDto();
    lunch.name = 'Lunch';
    this.totals.push(lunch);    
    var dinner = new FoodDto();
    dinner.name = 'Dinner';
    this.totals.push(dinner);    
    var snacks = new FoodDto();
    snacks.name = 'Snacks';
    this.totals.push(snacks);    
	}

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.category = params['id'];
    });
  }
}