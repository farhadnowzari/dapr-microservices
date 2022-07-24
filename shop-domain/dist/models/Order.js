import ModelBase from "./ModelBase";
export default class Order extends ModelBase {
    productId;
    quantity = 1;
    product = null;
    unitPrice = 0;
    /**
     *
     */
    constructor(obj = null) {
        super();
        if (obj) {
            Object.assign(this, obj);
        }
    }
}
//# sourceMappingURL=Order.js.map