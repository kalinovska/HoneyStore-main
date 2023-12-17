import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Order } from '../../models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = `${environment.apiUrl}/api`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/orders`);
  }

  getById(id: number): Observable<Order> {
    return this.http.get<Order>(`${this.apiUrl}/orders/` + id);
  }

  getOrdersByUserId(userId: number | undefined): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/orders/user/${userId}`);
  }

  post(order: Order) {
    return this.http.post(`${this.apiUrl}/orders`, order);
  }

  update(order: Order) {
    return this.http.put(`${this.apiUrl}/orders/` + order.id, order);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/orders/` + id);
  }
}
