import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserAuthService {

  baseUrl = environment.APIURL + 'Users/';

  constructor(private http: HttpClient) { }
    
  loginUser(model: any) {
    return this.http.post(this.baseUrl + 'Login', model)
      .pipe(
        map((response: any) => {
          const user = response;
          if(user) {
            localStorage.setItem('token', user.token);
          }
        })
      );
  }
}
