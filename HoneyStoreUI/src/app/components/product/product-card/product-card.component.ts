import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { CartItem, Product, Wish } from '../../../models';
import { AuthenticationService, CartItemService, WishService } from '../../../services';
import { Router } from '@angular/router';
import { FileHelper } from '../../../helpers';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [CommonModule, 
  MatCardModule,
  MatIconModule,
  MatButtonModule,
  MatDividerModule],
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.css'
})
export class ProductCardComponent implements OnInit {

  @Input() product: Product = new Product();
  public imageURL: any = null;

  constructor(private fileHelper: FileHelper,
    private cartSvc: CartItemService, 
    private wishSvc: WishService,
    private authSvc: AuthenticationService, 
    private snackBar: MatSnackBar,
    private router: Router) {        
   }

  ngOnInit() {
    this.imageURL = this.fileHelper.getImageSafeURL(this.product.productPhoto.fileBytes, this.product.productPhoto.fileName);
  }

  showDetail(productId: number) {
    this.router.navigate([`/detail/${productId}`]);
  }

  addToCart(productId: number, productName: string) {
    this.cartSvc.getItemsByUserId(this.authSvc.currentUserValue?.id).subscribe(data => {
      let existingCartItem: CartItem = data.filter(i => i.productId === productId)[0];
      
      if (existingCartItem) {
        existingCartItem.quantity++;
        this.cartSvc.update(existingCartItem).subscribe();
      } else {
        const cartItem = new CartItem(productId, this.authSvc.currentUserValue?.id, 1);
        this.cartSvc.post(cartItem).subscribe(data => {
          this.showNotification(`Продукт ${productName} додано в корзину`, 'Закрити')
       });
      }
    })
  }

  addToWish(productId: number, productName: string) {
    this.wishSvc.getWishesByUserId(this.authSvc.currentUserValue?.id).subscribe(data => {
      let existingWish: Wish = data.filter(i => i.productId === productId)[0];
      
      if (!existingWish) {
        const wish = new Wish(productId, this.authSvc.currentUserValue?.id);

       this.wishSvc.post(wish).subscribe(data => {
          this.showNotification(`Продукт ${productName} додано до списку вподобань`, 'Закрити')
       });
      }
    })
  }

  showNotification(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 5000,
    });
  }
}
