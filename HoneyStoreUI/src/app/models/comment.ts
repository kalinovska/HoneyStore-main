export class Comment {
    id: number;
    productId: number; 
    userId: number;
    firstName: string;
    lastName: string;
    email: string;
    createdOn: Date;
    mark: number;
    content: string;
    userName: string;

    constructor(userId: number = 1, productId:  number = 1, content: string = '', mark: number = 0) {
        this.id = 0;
        this.userId = userId;
        this.firstName = '';
        this.lastName = '';
        this.email = '';
        this.productId = productId;
        this.mark = mark;
        this.userName = '';
        this.content = content;
        this.createdOn = new Date(Date.now());
    }
}