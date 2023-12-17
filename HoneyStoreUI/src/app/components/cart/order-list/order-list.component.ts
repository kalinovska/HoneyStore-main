import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { Order } from '../../../models';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { AuthenticationService, OrderService } from '../../../services';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Router, RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-order-list',
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
  templateUrl: './order-list.component.html',
  styleUrl: './order-list.component.css'
})
export class OrderListComponent implements OnInit {
  displayedColumns: string[] = [
    'id',
    'firstName',
    'lastName',
    'email',
    'address',
    'status',
    'createdOn',
    'itemIds',
    'details'];

    @ViewChild(MatPaginator) paginator: MatPaginator | null = null;
    dataSource: MatTableDataSource<Order> = new MatTableDataSource<Order, MatPaginator>();
    @Input() listOrders: Order[] = [];
    @Input() pageSize: number = 12;

    constructor(private orderSvc: OrderService,
      private authSvc: AuthenticationService, 
      private snackBar: MatSnackBar,
      private router: Router) {
    }

    ngOnInit() {
      this.orderSvc.getOrdersByUserId(this.authSvc.currentUserValue?.id).subscribe(data => {
        this.listOrders = data;
        console.log(data)
        this.dataSource = new MatTableDataSource<Order>(data);
        this.dataSource.paginator = this.paginator;
      });    
    }
}
