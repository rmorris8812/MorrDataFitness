import { Component, OnInit, Inject } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Router, ActivatedRoute } from '@angular/router'
import { Observable, Subscriber } from 'rxjs';
import { tap, map, filter } from 'rxjs/operators';

import { RecipeDto } from '../services/recipe.dto';
import { RestContainer } from '../services/rest.container';
import { MorrDataService } from '../services/morr.recipes.service';

@Component({
  selector: 'app-recipes',
  templateUrl: 'recipes.component.html',
  styles: ['table { width: 100%;}']
})
export class RecipesComponent implements OnInit {
  displayedColumns: string[] = ['Name', 'Created', 'LastAccess', 'Servings', 'Calories'];

  index = 0;
  length = 100;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  pageEvent: PageEvent;

  category: string = '';
  recipes: RecipeDto[] = [];

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
    this.service.getRecipes(this.category, this.index, this.pageSize)
      .map(response => response.json())
      .subscribe(data => {
        if (data) {
          console.log(data);
          let container = data as RestContainer;
          if (container.errorCode === 200) {
            this.recipes = container.data as RecipeDto[];
          }
        }
      });
  }
}