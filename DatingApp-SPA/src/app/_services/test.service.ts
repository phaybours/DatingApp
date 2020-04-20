import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TestService {
  baseUrl = 'http://localhost:4400/api/Auth/';
  model: any = {};

  constructor(private http: HttpClient) { }

  get() {

  }

  post(model: any, endpoint: string) {
    return this.http.post(this.baseUrl + endpoint, model).pipe(
      map((response: any) => {
        // const user = response;
        // if (user) {
        //   localStorage.setItem('token', user.token);
        // }
        return response;
      })
    );
  }

}
