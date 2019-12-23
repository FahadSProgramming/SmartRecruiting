import { UserAuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  constructor(private authService: UserAuthService) { }

  ngOnInit() {
  }

  login() {
    this.authService.loginUser(this.model).subscribe(next  => {
      console.log('logged in!');
    }, error => {
      console.log('failed login');
    });
  }

}
