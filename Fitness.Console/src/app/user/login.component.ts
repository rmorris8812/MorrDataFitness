import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Router, ActivatedRoute, ParamMap } from '@angular/router'
import { Observable, Subscriber } from 'rxjs';
import { tap, map, filter } from 'rxjs/operators';

import { UserDto } from '../services/user.dto';
import { MorrDataService } from '../services/morr.recipes.service';
import { LoginDialog } from './login.dialog'

@Component({
  selector: 'app-login',
  templateUrl: 'login.component.html',
  styles: ['table { width: 100%;}']
})
export class LoginComponent implements OnInit {
  user: UserDto = new UserDto();

  /**
   * Ctor.
   * @param router - the router service to use to get url parameters.
   * @param lea - the service to use to interface to the ILS Rest API.
   * @param dialog - the dialog to use to generate a key.
   */
	constructor(@Inject(ActivatedRoute) private route: ActivatedRoute,
              @Inject(Router) private router: Router, 
              @Inject(MorrDataService) private service: MorrDataService,
              public dialog: MatDialog) {

	}

  ngOnInit() {
    this.openDialog();
  }

  openDialog(): void {

    const dialogRef = this.dialog.open(LoginDialog, {
      width: '500px',
      data: { email: "rmorris8812@gmail.com", password: "", service: this.service }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  authenticate()
  {
    this.service.authenticate(this.user.email, this.user.password);
  }
}