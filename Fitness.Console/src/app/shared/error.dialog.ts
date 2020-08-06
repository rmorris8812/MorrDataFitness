/*---------------------------------------------------------------------------
 * Generate.Key.Dialog.ts
 *---------------------------------------------------------------------------
 * Copyright (C) Ivanti Corporation 2019. All rights reserved.
 *
 * This file contains trade secrets of the Ivanti Corporation. No part
 * may be reproduced or transmitted in any form by any means or for any purpose
 * without the express written permission of the Ivanti Corporation.
 *---------------------------------------------------------------------------
 */
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface IErrorData {
	errorMessage: string;
}

@Component({
  selector: 'app-error',
  templateUrl: 'error.dialog.html',
  styles: []
})
export class ErrorDialog {
	errorMessage:string;
  	constructor(
    	public dialogRef: MatDialogRef<ErrorDialog>,
    	@Inject(MAT_DIALOG_DATA) public data: IErrorData) {
  		this.errorMessage = data.errorMessage;
  	}

  	OnCancel(): void {
    	this.dialogRef.close();
	}
}
