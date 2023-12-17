import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CommentComponent } from '../../comment/comment/comment.component';
import { AuthenticationService, CartItemService, CommentService, ProductService, WishService } from '../../../services';
import { CartItem, Product, Comment, Wish } from '../../../models';
import { ActivatedRoute } from '@angular/router';
import { CounterComponent } from '../../common/counter/counter.component';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatTabsModule } from '@angular/material/tabs';
import { MatBadgeModule } from '@angular/material/badge';
import { FileHelper } from '../../../helpers';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CommentDialogComponent } from '../../comment/comment-dialog/comment-dialog.component';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule,
    CounterComponent,
    CommentDialogComponent,
    CommentComponent,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    MatDividerModule,
    MatTabsModule,
    MatBadgeModule],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.css'
})
export class ProductDetailComponent implements OnInit {
  currentProduct: Product = new Product();
  productId: number = 0;
  productComments: Comment[] = [];
  cartItem: CartItem = new CartItem();
  quantity: number = 1;
  imageURL: any = null;

  constructor(private productSvc: ProductService,
    private authSvc: AuthenticationService,
    private cartSvc: CartItemService,
    private wishSvc: WishService,
    private commentSvc: CommentService,
    private fileHelper: FileHelper,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      this.productId = params['id'];
    });

    this.commentSvc.getCommentsByProductId(this.productId).subscribe(data => {
      this.productComments = data;
    });
  }

  ngOnInit(): void {
    this.productSvc.getById(this.productId).subscribe(
      data => {
        this.currentProduct = data;        
        this.imageURL = this.fileHelper.getImageSafeURL(this.currentProduct.productPhoto.fileBytes, this.currentProduct.productPhoto.fileName);
      },
      error => {
        if (error.status == 401) {
          this.productComments.length
        }
    });
  }

  onQuantityChanged(value: number) {
    this.quantity = value;
  }

  onCommentsChanged(event: Event) {
    this.commentSvc.getCommentsByProductId(this.productId).subscribe(data => {
      this.productComments = data;
    });
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
