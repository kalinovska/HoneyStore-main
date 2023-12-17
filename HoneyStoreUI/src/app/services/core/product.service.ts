import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Product } from '../../models';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = `${environment.apiUrl}/api`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/products`);
  }

  getById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/products/` + id);
  }

  searchByName(name: string) {
    return this.http.get<Product[]>(`${this.apiUrl}/products/search/${name}`);
  }

  post(product: Product, imageFile: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', imageFile, imageFile.name);
    formData.append('jsonProduct', JSON.stringify(product));

    return this.http.post(`${this.apiUrl}/products`, formData);
  }

  update(product: Product) {
    return this.http.put(`${this.apiUrl}/products/` + product.id, product);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/products/` + id);
  }

  getProductsByCategoryId(id: number): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/products/category/` + id);
  }
}
