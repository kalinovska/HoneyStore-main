import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator'
import { MatTableDataSource, MatTableModule } from '@angular/material/table'
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { User } from '../../../models';
import { UserService } from '../../../services';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-admin-user-list',
  standalone: true,
  imports: [CommonModule,
    MatSelectModule,
    MatIconModule,
    MatPaginatorModule,
    MatTableModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSnackBarModule],
  templateUrl: './admin-user-list.component.html',
  styleUrl: './admin-user-list.component.css'
})
export class AdminUserListComponent {

  displayedColumns: string[] = [
    'id',
    'firstName',
    'lastName',
    'role',
    'email'
  ];

  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;
  dataSource: MatTableDataSource<User> = new MatTableDataSource<User, MatPaginator>();
  listUsers: User[] = [];
  pageSize: number = 12;

  constructor(private userSvc: UserService) {
    this.userSvc.getAll().subscribe(data => {
      this.dataSource = new MatTableDataSource<User>(data);
      this.dataSource.paginator = this.paginator;
    });
  }
}
