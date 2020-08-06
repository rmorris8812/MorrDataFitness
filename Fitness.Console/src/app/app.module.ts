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
import { LoginComponent } from './user/login.component'
import { ErrorDialog } from './shared/error.dialog';
import { RecipesComponent } from './recipes/recipes.component';
import { FoodComponent } from './food/food.component';
import { DietComponent } from './diet/diet.component';
import { RouteReuseStrategy } from "@angular/router";
import { CustomReuseStrategy } from "./custom.reuse.strategy";

import { SideNavComponent } from "./sidenav.component";
import { MenuComponent } from "./menu.component";

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ErrorDialog,
    RecipesComponent,
    FoodComponent,
    DietComponent,
    SideNavComponent,
    MenuComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forRoot([
      { path: 'user/login', component: LoginComponent},
      { path: 'recipes/:id', component: RecipesComponent},
      { path: 'food/:id', component: FoodComponent},
      { path: 'diet/:id', component: DietComponent},
      { path: '', component: LoginComponent},
      { path: '**', component: LoginComponent}
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
export class AppModule { }
