import { Errors } from './../entities/error.entity';
import { MatSnackBar } from '@angular/material/snack-bar';

export function errorHandler(httpError: Errors, snackBar: MatSnackBar){
  console.log(httpError);
  snackBar.open(httpError.errors[0].errorMessage, undefined, { duration: 5000});
}
