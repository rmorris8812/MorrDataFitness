declare var require:any;
let parseString = require('xml2js').parseString;

import { Injectable, Inject } from '@angular/core';
import { Http, Headers, Response, ResponseOptions, RequestOptions } from '@angular/http';

import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { UserDto } from './user.dto';
import { TokenDto } from './token.dto'

import { Environment } from '../environment';

@Injectable({
  providedIn: 'root',
})
export class MorrDataService {
	baseUrl: string = 'http://localhost:5000/api/';
	user: UserDto = new UserDto();
	isLoggedIn: boolean = false;

	constructor(@Inject(Http) private http : Http) {
	}

	authenticate(email: string, password: string) {
		var path = this.baseUrl + 'user';
		var dto = new UserDto();
		dto.Email = email;
		dto.Password = password;
		this.http.post(path, dto)
			.map(response => response.json())
	          .subscribe(data => {
	          if (data) {
	              console.log(data);
	              let user = data as UserDto;
	              if (user.Token !== undefined) {
	                console.log("Got the token : " + user.Token);
	                this.user = user;
	                loggedIn(true);
	              }
	          }
	        });
	}
	
	getRecipes(category: string, skip: number, top: number) {
		let headers = new Headers();
		headers.append("Token", this.user.Token);
		headers.append('Content-Type', 'application/json');
	    let options = new RequestOptions({ headers: headers });
    	let path = this.baseUrl + 'recipe?category=' + category + '&skip=' + skip + '&top=' + top;
        return this.http.get(path, options);
    }
}