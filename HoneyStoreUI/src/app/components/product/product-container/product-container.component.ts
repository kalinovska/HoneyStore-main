import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { AuthenticationService, ProductService } from '../../../services';
import { Product } from '../../../models';
import { ProductListComponent } from '../product-list/product-list.component';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CategoryCardComponent } from '../../category/category-card/category-card.component';

@Component({
  selector: 'app-product-container',
  standalone: true,
  imports: [CommonModule,
  ProductListComponent,
  CategoryCardComponent,
  MatIconModule,
  MatDividerModule,
  MatSelectModule,
  MatFormFieldModule],
  templateUrl: './product-container.component.html',
  styleUrl: './product-container.component.css'
})
export class ProductContainerComponent {
  products: Product[] = [];
  productPerPage: number = 12;

  constructor(private route: ActivatedRoute, private productSvc: ProductService, public authSvc: AuthenticationService) {
    let name = null;
    this.route.queryParams.subscribe(params => {
      name = params['name'];
    });
    if (name) {
      this.productSvc.searchByName(name).subscribe(data => {
        this.products = data;
      });
    } else {
      this.productSvc.getAll().subscribe(data => {
        this.products = data;
      });
    }
  }

  onCategorySelected(categoryId: any) {
    this.productSvc.getProductsByCategoryId(categoryId).subscribe(data => {
      this.products = data;
    })
  }

  changeSortOption(option: string) {

    if (option === "priceAscendant") {
      this.products = this.products.sort((a, b) =>
        (a.price > b.price) ?
          1 : ((b.price > a.price) ? -1 : 0)).slice();;
    }
    else if (option === "priceDescendant") {
      this.products = this.products.sort((a, b) =>
        (a.price > b.price) ?
          1 : ((b.price > a.price) ? -1 : 0))
        .reverse().slice();;
    }
    else if (option === "nameAscendant") {
      this.products = this.products.sort((a, b) =>
        (a.name > b.name) ?
          1 : ((b.name > a.name) ? -1 : 0)).slice();;
    }
    else if (option === "nameDescendant") {
      this.products = this.products.sort((a, b) =>
        (a.name > b.name) ?
          1 : ((b.name > a.name) ? -1 : 0))
        .reverse().slice();
    }
  }

  changeSizePage(value: number) {
    this.productPerPage = value;
  }
}
