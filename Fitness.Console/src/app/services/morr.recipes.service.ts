declare var require:any;
let parseString = require('xml2js').parseString;

import { Injectable, Inject } from '@angular/core';
import { Http, Headers, Response, ResponseOptions, RequestOptions } from '@angular/http';

import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { UserDto } from './user.dto';
import { TokenDto } from './token.dto'

export class MorrDataService {
	baseUrl : string = 'http://localhost:5000/api/';
	token : string = '84f3hnjnr32f7i4fhi4ffijfnjerbvekj==';

	constructor(@Inject(Http) private http : Http) {
	}

	authenticate(email: string, password: string) {
		var path = this.baseUrl + 'user';
		var dto = new UserDto();
		dto.email = email;
		dto.password = password;
		this.http.post(path, dto).map(response => response.json())
      		.subscribe(data => {
        	if (data) {
          		console.log(data);
          		let container = data as TokenDto;
          		if (container.token !== null) {
            		this.token = container.token;
          		}
        	}
      	});
	}
	
	getRecipes(category: string, skip: number, top: number) {
		let headers = new Headers();
		headers.append("Token", this.token);
		headers.append('Content-Type', 'application/json');
	    let options = new RequestOptions({ headers: headers });
    	let path = this.baseUrl + 'recipe?category=' + category + '&skip=' + skip + '&top=' + top;
        return this.http.get(path, options);
    }
}