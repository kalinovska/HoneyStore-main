export class Order {
    id: number;
    userId: number | undefined;    
    firstName: string | undefined;
    lastName: string | undefined;
    phoneNumber: string | undefined;
    email: string | undefined;
    address: string | undefined;
    details: string | undefined;
    deliveryMethod: string | undefined;
    paymentMethod: string | undefined;
    cartItemIds: number[];
    productIds: number[];
    status: string;
    createdOn: Date | undefined;

    constructor(userId: number = 1) {
        this.id = 0;
        this.firstName = '';        
        this.lastName = '';
        this.phoneNumber = '';
        this.email = '';
        this.userId = userId;
        this.cartItemIds = [];
        this.productIds = [];
        this.status = '';
        this.createdOn = new Date(Date.now());
    }
}