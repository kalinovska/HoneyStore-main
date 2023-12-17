import { Producer } from './producer';
import { Category } from './category';
import { ProductPhoto } from './product-photo';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

export class Product {
    id: number;
    imageUrl: string | undefined;
    name: string;    
    mark: number;
    description: string | undefined;
    price: number;
    quantity: number;
    weight: number;
    commentsEnabled: boolean | undefined;
    producer: Producer;
    productPhotoId: number;
    productPhoto: ProductPhoto;
    categoryId: number | undefined;
    category: Category;

    constructor() {
        this.id = 0;
        this.name = '';   
        this.imageUrl = '';
        this.mark = 0;
        this.description = '';
        this.price = 10;
        this.quantity = 1;
        this.weight = 0;       
        this.commentsEnabled = true;
        this.productPhotoId = 0;
        this.productPhoto = new ProductPhoto();
        this.producer = new Producer();
        this.categoryId = undefined;
        this.category = new Category();
    }
}