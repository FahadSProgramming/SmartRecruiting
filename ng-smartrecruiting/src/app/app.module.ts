
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';

// custom components
import { LoginComponent } from './login/login.component';

// Custom services
import { UserAuthService } from './services/auth.service';

@NgModule({
   declarations: [
      AppComponent,
      LoginComponent
   ],
   imports: [
      BrowserModule,
      FormsModule,
      HttpClientModule
   ],
   providers: [
      UserAuthService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
