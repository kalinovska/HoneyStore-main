import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { User } from '../../models';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = `${environment.apiUrl}/api`;

  constructor(private http: HttpClient) { }

  getAll() {
      return this.http.get<User[]>(`${this.apiUrl}/users`);
  }

  getById(id: number) {
      return this.http.get(`${this.apiUrl}/users/` + id);
  }

  register(user: User) {
      return this.http.post(`${this.apiUrl}/account/register`, user);
  }

  update(user: User) {
      return this.http.put(`${this.apiUrl}/users/` + user.id, user);
  }

  delete(id: number) {
      return this.http.delete(`${this.apiUrl}/users/` + id);
  }
}
