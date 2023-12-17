import { Product } from './product';

export class CartItem {
    id: number;
    productId: number;
    product: Product;
    userId: number | undefined;
    quantity: number;
    createdOn: Date | undefined;    
    orderId: number | null;

    constructor(productId: number = 1, userId: number = 1, quantity: number = 1) {
        this.id = 0;
        this.productId = productId;
        this.product = new Product();
        this.userId = userId;   
        this.quantity = quantity;
        this.createdOn = new Date();               
        this.orderId = null;
    }
}