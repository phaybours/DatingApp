import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { TestService } from '../_services/test.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService, private testService: TestService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log(next);
    }, error => {
      console.log(error);
    });

    // this.testService.post(this.model, 'login').subscribe(next => {
    //   const user = next;
    //   if (user) {
    //     localStorage.setItem('token', user.token);
    //   }
    //   console.log(next);
    // }, error => {
    //   console.log(error);
    // });
  }
  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }

  logout() {
    localStorage.removeItem('token');
    console.log('Logged Out');
  }

}
