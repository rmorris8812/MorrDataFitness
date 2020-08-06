import { Component, OnInit, Inject } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: 'menu.component.html',
  styles: ['table { width: 100%;}']
})
export class MenuComponent implements OnInit {

  /**
   * Ctor.
   */
	constructor() {
	}

  ngOnInit() {
    // nada for now
  }
}