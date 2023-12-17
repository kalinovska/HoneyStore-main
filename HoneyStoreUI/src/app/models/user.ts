export class User {
    id: number | undefined;
    email: string | undefined;
    password: string | undefined;
    firstName: string | undefined;
    lastName: string | undefined;
    roleName: string | undefined;

    constructor() {
        this.id = 0;
        this.email = '';   
        this.password = '';
        this.firstName = '';        
        this.lastName = '';
        this.roleName = '';
    }
}
