import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CartItem } from '../../models';

@Injectable({
  providedIn: 'root'
})
export class CartItemService {
  private apiUrl = `${environment.apiUrl}/api`;

  cartItemsValue: CartItem[] = [];
  constructor(private http: HttpClient) { }

  public get totalSumValue(): number {
    return this.computeSum();
  }

  private computeSum() : number {
    var totalSum = 0;
    this.cartItemsValue.forEach((ci: CartItem) => totalSum += ci.quantity * ci.product.price);
    return totalSum;
  }

  getAll(): Observable<CartItem[]> {
    return this.http.get<CartItem[]>(`${this.apiUrl}/cartitems`);
  }

  getItemsByUserId(userId: number | undefined): Observable<CartItem[]> {
    return this.http.get<CartItem[]>(`${this.apiUrl}/cartitems/user/${userId}`);
  }

  getById(id: number): Observable<CartItem> {
    return this.http.get<CartItem>(`${this.apiUrl}/cartitems/` + id);
  }

  post(cartItem: CartItem): Observable<CartItem> {
    return this.http.post<CartItem>(`${this.apiUrl}/cartitems`, cartItem);
  }

  update(cartItem: CartItem) {
    return this.http.put(`${this.apiUrl}/cartitems/` + cartItem.id, cartItem);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/cartitems/` + id);
  }
}
