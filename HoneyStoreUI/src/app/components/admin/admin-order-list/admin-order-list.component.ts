import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Order } from '../../../models';
import { MatDialog } from '@angular/material/dialog';
import { OrderService } from '../../../services';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Router, RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-admin-order-list',
  standalone: true,
  imports: [CommonModule,
    MatPaginatorModule,
    MatSnackBarModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    RouterModule,
    MatSelectModule,
    MatFormFieldModule],
  templateUrl: './admin-order-list.component.html',
  styleUrl: './admin-order-list.component.css'
})
export class AdminOrderListComponent implements OnInit {
  displayedColumns: string[] = [
    'id',
    'firstName',
    'lastName',
    'email',
    'phone',
    'address',
    'status',
    'createdOn',
    'itemIds',
    'details',
    'deleteOption'];

    @ViewChild(MatPaginator) paginator: MatPaginator | null = null;
    dataSource: MatTableDataSource<Order> = new MatTableDataSource<Order, MatPaginator>();
    @Input() listOrders: Order[] = [];
    @Input() pageSize: number = 12;

    constructor(private orderSvc: OrderService, 
      private snackBar: MatSnackBar,
      private router: Router) {
    }

    ngOnInit() {
      this.orderSvc.getAll().subscribe(data => {
        this.listOrders = data;
        this.dataSource = new MatTableDataSource<Order>(data);
        this.dataSource.paginator = this.paginator;
      });    
    }
  
    ngOnChanges() {
      if (this.dataSource) {
        this.dataSource.data = this.listOrders;
      }
    }

    onStatusUpdated(status: string, order: Order) {
      order.status = status
    }

    updateStatus(order: Order) {
      console.log(order)
      this.orderSvc.update(order).subscribe();
    }

    remove(order: Order) {
      this.orderSvc.delete(order.id).subscribe(() => {
        this.showNotification(`Замовлення користувача "${order.firstName}" "${order.lastName}" було успішно видалено.`, 'Закрити');
  
        this.refreshDataSource();
      });
    }

    showNotification(message: string, action: string) {
      this.snackBar.open(message, action, {
        duration: 5000,
      });
    }

    refreshDataSource() {
      this.orderSvc.getAll().subscribe((data: Order[]) => {
        this.dataSource.data = data;
      });
    }
}
