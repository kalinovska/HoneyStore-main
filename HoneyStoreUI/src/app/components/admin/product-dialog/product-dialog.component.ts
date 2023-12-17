import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormMode } from '../../../constants/form-mode';
import { Category, Product } from '../../../models';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthenticationService, CategoryService, ProductService } from '../../../services';
import { ProductDialogData } from '../../../helpers/product-dialog-data';
import { FileHelper } from '../../../helpers';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatCheckboxModule } from '@angular/material/checkbox'
import { EditorModule } from '@tinymce/tinymce-angular';

@Component({
  selector: 'app-product-dialog',
  standalone: true,
  imports: [CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
    MatDialogModule,
    MatCardModule,
    MatDividerModule,
    MatSnackBarModule,
    MatCheckboxModule,
    EditorModule],
  templateUrl: './product-dialog.component.html',
  styleUrl: './product-dialog.component.css'
})
export class ProductDialogComponent implements OnInit {
  public categorySource: Array<Category> = [];
  public currentProduct: Product = new Product();
  public imageFile: File = new File([], '');
  public imageURL: any = '../../../../assets/images/photo_upload.svg';

  productForm: FormGroup = this.createFormGroup(this.currentProduct);
  formMode: FormMode;
  dialogTitle: string = '';

  constructor(private productSvc: ProductService,
    private authSvc: AuthenticationService,
    private categorySvc: CategoryService,
    private fileHelper: FileHelper,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<ProductDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private dialogData: ProductDialogData) {

    this.formMode = dialogData.formMode;
    this.categorySvc.getAll().subscribe(data => {
      this.categorySource = data;
    });    
  }

  ngOnInit() {
    switch (this.formMode) {
      case FormMode.New: {
        this.dialogTitle = "Новий продукт"
        this.currentProduct = new Product();
        this.productForm = this.createFormGroup(this.currentProduct);        
        break;
      }
      case FormMode.Edit: {
        this.dialogTitle = `Редагувати продукт# ${this.dialogData.productId}`;
        this.productSvc.getById(this.dialogData.productId).subscribe((product: Product) => {
          this.currentProduct = product;
          this.productForm = this.createFormGroup(this.currentProduct);
          this.imageURL = this.fileHelper.getImageSafeURL(this.currentProduct.productPhoto.fileBytes, this.currentProduct.productPhoto.fileName);
        });
        break;
      }
    }
  }

  onFileUpload() {
    const fileUpload = document.getElementById('fileUpload') as HTMLInputElement;
    fileUpload.onchange = () => {
      if(fileUpload.files)
      {
        this.imageFile = fileUpload.files[0];
        this.setImageURL(this.imageFile);
      }      
    };
    fileUpload.click();
  }

  onSave() {
    if (this.productForm.invalid || (!this.imageFile || !this.imageURL)) {
      this.showNotification('Помилка! Некоректно введені дані.', 'Закрити')
      return;
    }
    
    this.populateProductData();

    switch (this.formMode) {
      case FormMode.New: {
        this.productSvc.post(this.currentProduct, this.imageFile).subscribe((data: any) => {
          this.showNotification(`Успішно додано новий продукт # ${data.id}`, 'Закрити')
          this.dialogRef.close();
        });        
        break;
      }
      case FormMode.Edit: {
        this.productSvc.update(this.currentProduct).subscribe(() => {
          this.showNotification(`Продукт # ${this.currentProduct.id} успішно збережено.`, 'Закрити')
        });
        break;
      }
    }
  }

  isNewMode(): boolean {
    return this.formMode === FormMode.New;
  }

  populateProductData() {
    const productValue = this.productForm.value;
    this.currentProduct.name = productValue.name;
    this.currentProduct.producer.name = productValue.producerName;
    this.currentProduct.categoryId = productValue.category
    this.currentProduct.price = productValue.price;
    this.currentProduct.quantity = productValue.quantity;
    this.currentProduct.weight = productValue.weight;
    this.currentProduct.description = productValue.description;
  }

  createFormGroup(product: Product) {
    return this.formBuilder.group({
      name: [product.name],
      producerName: [product.producer.name],
      category: [product.categoryId],
      price: [product.price, [Validators.min(0), Validators.max(1000000), Validators.pattern("^[0-9]+(.[0-9]{0,2})?$")]],
      quantity: [product.quantity, [Validators.min(0), Validators.max(100000), Validators.pattern("^[0-9]+(.[0-9]{0,2})?$")]],
      weight: [product.weight, [Validators.min(0), Validators.max(100000), Validators.pattern("^[0-9]+(.[0-9]{0,2})?$")]],
      description: [product.description],
      commentsEnabled: [product.commentsEnabled]
    });
  }

  setImageURL(file: File | Blob) {
    var reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = (_event) => {
      this.imageURL = reader.result;
    };
  }

  showNotification(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 5000,
    });
  }
}
