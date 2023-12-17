import { Injectable } from '@angular/core';
import { Wish } from '../../models';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WishService {
  private apiUrl = `${environment.apiUrl}/api`;
  
  wishListValue: Wish[] = [];

  constructor(private http: HttpClient) { }

  getAll(): Observable<Wish[]>{
    return this.http.get<Wish[]>(`${this.apiUrl}/wishes`);
  }

  getWishesByUserId(userId: number | undefined): Observable<Wish[]> {
    return this.http.get<Wish[]>(`${this.apiUrl}/wishes/user/${userId}`);
  }

  post(wish: Wish): Observable<Wish> {
    return this.http.post<Wish>(`${this.apiUrl}/wishes`, wish);
  }

  update(wish: Wish) {
    return this.http.put(`${this.apiUrl}/wishes/`, wish);
  }

  delete(productId: number, userId: number | undefined) {
    return this.http.delete(`${this.apiUrl}/wishes/product/${productId}/user/${userId}`);
  }
}
