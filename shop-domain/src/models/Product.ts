import ModelBase from "./ModelBase";

export default class Product extends ModelBase {
    name: string | null = null;
    quantity: number = 0;
    unitPrice: number = 0;

    constructor(input: unknown | Product) {
        super();
        Object.assign(this, input);
    }
}