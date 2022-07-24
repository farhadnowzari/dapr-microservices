import ModelBase from "./ModelBase";
import Product from "./Product";

export default class Order extends ModelBase {
    productId!: string;
    quantity: number = 1;
    product: Product | null = null;
    unitPrice: number = 0;
    /**
     *
     */
    constructor(obj: unknown | Order | null = null) {
        super();
        if (obj) {
            Object.assign(this, obj);
        }
    }
}