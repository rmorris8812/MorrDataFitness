import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree } from '@angular/router';
import { MorrDataService } from '../services/morr.data.service';
import { E
	nvironment } from '../environment';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  
  constructor(private router: Router, 
  	          private service: MorrDataService) {

  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): true|UrlTree {
  	
  	const url: string = state.url;

    console.log('AuthGuard#canActivate called');
    return this.checkLogin(url);
  }

  checkLogin(url: string): true|UrlTree {
  	console.log("User is logged in : " + _environment.isUserLoggedIn);
    if (_environment.isUserLoggedIn) {
    	console.log("User already logged in"); 
    	return true; 
	}
    // Redirect to the login page
    return this.router.parseUrl('/user/login');
  }
}
