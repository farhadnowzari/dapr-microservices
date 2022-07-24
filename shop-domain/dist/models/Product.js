import ModelBase from "./ModelBase";
export default class Product extends ModelBase {
    name = null;
    quantity = 0;
    unitPrice = 0;
    constructor(input) {
        super();
        Object.assign(this, input);
    }
}
//# sourceMappingURL=Product.js.map