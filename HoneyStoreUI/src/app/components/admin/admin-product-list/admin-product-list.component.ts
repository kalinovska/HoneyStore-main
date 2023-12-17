import { Component, Input, OnChanges, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Product } from '../../../models';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { ProductService } from '../../../services';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Router, RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatSlideToggleChange, MatSlideToggleModule } from '@angular/material/slide-toggle';
import { ProductDialogComponent } from '../product-dialog/product-dialog.component';
import { FormMode } from '../../../constants/form-mode';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';

@Component({
  selector: 'app-admin-product-list',
  standalone: true,
  imports: [CommonModule,
  MatPaginatorModule,
  MatSnackBarModule,
  MatTableModule,
  MatButtonModule,
  MatSlideToggleModule,
  MatDialogModule,
  RouterModule,
  ProductDialogComponent],
  templateUrl: './admin-product-list.component.html',
  styleUrl: './admin-product-list.component.css'
})
export class AdminProductListComponent implements OnInit, OnChanges {
  displayedColumns: string[] = [
    'id',
    'name',
    'price',
    'editOption',
    'deleteOption',
    'detailOption',
    'commentingOption'];

  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;
  dataSource: MatTableDataSource<Product> = new MatTableDataSource<Product, MatPaginator>();
  @Input() listProducts: Product[] = [];
  @Input() pageSize: number = 12;

  constructor(public dialog: MatDialog,
    private productSvc: ProductService, 
    private snackBar: MatSnackBar,
    private router: Router) {
  }

  ngOnInit() {
    this.productSvc.getAll().subscribe(data => {
      this.listProducts = data;
      this.dataSource = new MatTableDataSource<Product>(data);
      this.dataSource.paginator = this.paginator;
    });    
  }

  ngOnChanges() {
    if (this.dataSource) {
      this.dataSource.data = this.listProducts;
    }
  }

  openCreationDialog(): void {
    const dialogRef = this.dialog.open(ProductDialogComponent, {
      maxHeight: '750px',
      maxWidth: '1000px',
      height: '100%',
      width: '100%',
      data: { productId: null, formMode: FormMode.New}
    });

    dialogRef.afterClosed().subscribe(() => {this.refreshDataSource();});
  }

  openEditDialog(productId: number): void {
    const dialogRef = this.dialog.open(ProductDialogComponent, {
      maxHeight: '750px',
      maxWidth: '1000px',
      height: '100%',
      width: '100%',
      data: { productId: productId, formMode: FormMode.Edit}
    });

    dialogRef.afterClosed().subscribe(() => {this.refreshDataSource();});
  }

  remove(product: Product) {
    this.productSvc.delete(product.id).subscribe(() => {
      this.snackBar.open(`Продукт "${product.name}" був успішно видалений.`, 'Закрити', {
        duration: 3000,
      });

      this.refreshDataSource();
    });
  }

  openDetail(id: number) {
    this.router.navigate([`/detail/${id}`]);
  }

  toggleCommenting(event: MatSlideToggleChange, product: Product) {
     product.commentsEnabled = event.checked;
     this.productSvc.update(product).subscribe();
  }

  refreshDataSource() {
    this.productSvc.getAll().subscribe((data: Product[]) => {
      this.dataSource.data = data;
    });
  }
}
