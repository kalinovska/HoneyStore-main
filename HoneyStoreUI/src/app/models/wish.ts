import { Product } from './product';

export class Wish {
    id: number | undefined;
    userId: number | undefined;
    quantity: number;
    createdOn: Date | undefined;
    productId: number;
    product: Product;

    constructor(productId: number = 1, userId: number | undefined = 1, quantity: number = 1) {
        this.id = 0;
        this.productId = productId;
        this.product = new Product();
        this.userId = userId;   
        this.quantity = quantity;
        this.createdOn = new Date(Date.now());
    }
}