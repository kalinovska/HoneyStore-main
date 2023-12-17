import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { User } from '../../models';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private apiUrl: string = 'https://localhost:7114/api'
  
  private currentUserSubject: BehaviorSubject<User|null>;
  public currentUser: Observable<User|null>;

  constructor(private http: HttpClient) {
      this.currentUserSubject = new BehaviorSubject<User|null>(JSON.parse(`${localStorage.getItem('currentUser')}`));
      this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User|null {
      return this.currentUserSubject.value;
  }

  login(email: string, password: string) {
      return this.http.post<any>(`${this.apiUrl}/account/login`, { email, password })
          .pipe(map(response => {
              // login successful if there's a jwt token in the response
              if (response && response.access_token) {
                  // store user details and jwt token in local storage to keep user logged in between page refreshes
                  localStorage.setItem('access_token', JSON.stringify(response.access_token));
                  localStorage.setItem('currentUser', JSON.stringify(response.user));
                  this.currentUserSubject.next(response.user);
              }

              return response.user;
          }));
  }

  logout() {
      // remove user from local storage to log user out
      localStorage.removeItem('currentUser');
      localStorage.removeItem('access_token');
      this.currentUserSubject.next(null);
  }
}
