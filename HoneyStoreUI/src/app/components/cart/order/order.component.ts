import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDividerModule } from '@angular/material/divider';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';
import { MatRadioModule } from '@angular/material/radio';
import { Order } from '../../../models';
import { CartComponent } from '../cart/cart.component';
import { Router, RouterModule } from '@angular/router';
import { first } from 'rxjs';
import { AuthenticationService, CartItemService, OrderService } from '../../../services';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [CommonModule,
    CartComponent,
    RouterModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
    MatCardModule,
    MatDividerModule,
    MatSnackBarModule,
    MatCheckboxModule,
    MatRadioModule],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent {
  currentOrder: Order = new Order(); 
  orderForm: FormGroup = this.createFormGroup(this.currentOrder);

  constructor(public cartItemSvc: CartItemService,
    private authSvc: AuthenticationService,
    private orderSvc: OrderService,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar,
    private router: Router) {    
  }

  onSubmit() {
    if (this.orderForm.invalid) {
      return;
    }

    this.populateOrderData();

    this.orderSvc.post(this.currentOrder)
      .subscribe(data => {
        this.showNotification('Ваше замовлення успішно створено.', 'Закрити');
        this.router.navigate(['/orders'])
        this.cartItemSvc.cartItemsValue = []
      },
      error => {
        this.showNotification('Помилка! Не вдалося створити замовлення.', 'Закрити')
      });
    
  }
  
  get controls() { return this.orderForm.controls; }

  populateOrderData() {
    const orderValue = this.orderForm.value;
    this.currentOrder.userId = this.authSvc.currentUserValue?.id;
    this.currentOrder.firstName = orderValue.firstName;
    this.currentOrder.lastName = orderValue.lastName;
    this.currentOrder.phoneNumber = orderValue.phoneNumber;
    this.currentOrder.email = orderValue.email
    this.currentOrder.address = orderValue.address;
    this.currentOrder.details = orderValue.details;
    this.currentOrder.deliveryMethod = orderValue.deliveryMethod;
    this.currentOrder.paymentMethod = orderValue.paymentMethod;
    this.currentOrder.cartItemIds = this.cartItemSvc.cartItemsValue.map(i => i.id);
  }

  createFormGroup(order: Order) {
    return this.formBuilder.group({
      firstName: [this.authSvc.currentUserValue?.firstName, [Validators.required]],
      lastName: [this.authSvc.currentUserValue?.lastName, [Validators.required]],
      phoneNumber: [order.phoneNumber],
      email: [this.authSvc.currentUserValue?.email, [Validators.required, Validators.email]],
      address: [order.address, [Validators.required]],
      details: [order.details],
      privacyPolicy: [false],
      deliveryMethod: ['Нова пошта'],
      paymentMethod: ['Готівкою при отриманні']
    });
  }

  showNotification(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 5000,
    });
  }
}
