import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router'

import { ErrorDialog } from '../shared/error.dialog';
import { UserDto } from '../services/user.dto';
import { TokenDto } from '../services/token.dto'
import { MorrDataService } from '../services/morr.data.service';

export interface IDialogData {
	email: string;
	password: string;
	service: MorrDataService;
}

@Component({
  selector: 'app-login-dlg',
  templateUrl: 'login.dialog.html'
})
export class LoginDialog {
	dlgData: IDialogData;

  	constructor(
    	public dialogRef: MatDialogRef<LoginDialog>,
    	@Inject(MAT_DIALOG_DATA) public data: IDialogData,
      @Inject(Router) private router: Router,
    	public errorDialog: MatDialog) {
  		
  		this.dlgData = data;
  	}

  onAuthenticate() : void
  {
    this.dlgData.service.authenticate(this.dlgData.email, this.dlgData.password);
    this.router.parseUrl('/home');
    this.dialogRef.close();
  }
}