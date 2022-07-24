import ModelBase from "./ModelBase";
export default class Product extends ModelBase {
    name: string | null;
    quantity: number;
    unitPrice: number;
    constructor(input: unknown | Product);
}
