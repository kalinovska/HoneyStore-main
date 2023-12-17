import { FormMode } from '../constants/form-mode';

export class ProductDialogData {
    productId: number;
    formMode: FormMode;

    constructor(){
        this.productId = 0;
        this.formMode = FormMode.View;
    }
}