import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { 
  MatCheckboxModule, 
  MatButtonModule, 
  MatIconModule, 
  MatMenuModule, 
  MatPaginatorModule, 
  MatFormFieldModule, 
  MatInputModule,
  MatTableModule,
  MatDialogModule,
  MatSidenavModule,
  MatListModule
} from '@angular/material';

import { AppComponent } from './app.component';
import { AuthGuard } from './services/auth.guard';
import { LoginComponent } from './user/login.component'
import { LoginDialog } from './user/login.dialog'
import { ErrorDialog } from './shared/error.dialog';
import { HomeComponent } from './home/home.component';
import { RecipesComponent } from './recipes/recipes.component';
import { FoodComponent } from './food/food.component';
import { DietComponent } from './diet/diet.component';
import { RouteReuseStrategy } from "@angular/router";
import { CustomReuseStrategy } from "./custom.reuse.strategy";
import { SideNavComponent } from "./sidenav.component";
import { MenuComponent } from "./menu.component";

import { Environment } from './environment';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    LoginDialog,
    ErrorDialog,
    HomeComponent,
    RecipesComponent,
    FoodComponent,
    DietComponent,
    SideNavComponent,
    MenuComponent
  ],
  entryComponents: [ 
    LoginDialog 
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot([
      { path: 'user/login', component: LoginComponent},
      { path: 'recipes/:id', component: RecipesComponent, canActivate: [AuthGuard]},
      { path: 'food/:id', component: FoodComponent, canActivate: [AuthGuard]},
      { path: 'diet/:id', component: DietComponent, canActivate: [AuthGuard]},
      { path: '', component: HomeComponent, canActivate: [AuthGuard]},
      { path: '**', component: HomeComponent, canActivate: [AuthGuard]}
      ], { useHash: true }),
    BrowserModule,
    BrowserAnimationsModule,
    MatCheckboxModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatDialogModule,
    MatSidenavModule,
    MatListModule,
    FormsModule,
    HttpModule
  ],
  providers: [
    { provide: RouteReuseStrategy, useClass: CustomReuseStrategy }  
  ],
  bootstrap: [AppComponent]
})

export class AppModule {
  var _environment:Environment = new Environment(); 
  export function isLoggedIn() {
    return _environment.isLoggedIn;
  }
  export function loggedIn(value: boolean) {
    _environment.isLoggedIn = value;
  }
}
