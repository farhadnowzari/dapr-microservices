import ModelBase from "./ModelBase";
import Product from "./Product";
export default class Order extends ModelBase {
    productId: string;
    quantity: number;
    product: Product | null;
    unitPrice: number;
    /**
     *
     */
    constructor(obj?: unknown | Order | null);
}
