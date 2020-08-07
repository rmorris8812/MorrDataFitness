import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ErrorDialog } from '../shared/error.dialog';

import { UserDto } from '../services/user.dto';
import { MorrDataService } from '../services/morr.recipes.service';

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
    	public errorDialog: MatDialog) {
  		
  		this.dlgData = data;
  	}

  OnAuthenticate()
  {
    this.dlgData.service.authenticate(this.dlgData.email, this.dlgData.password);
  }
}