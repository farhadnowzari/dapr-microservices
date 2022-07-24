import { Order } from "@shop/domain/dist";
import orders from "../endpoints/orders";

export default class OrdersController {
    createOrder(productId: string): Promise<number> {
        return new Promise<number>((resolve, reject) => {
            axios({
                url: orders.create.url,
                method: orders.create.method,
                data: {
                    productId
                }
            }).then(response => {
                resolve(parseInt(response.data));
            })
                .catch(e => reject(e));
        });
    }

    getOrder(orderId: string): Promise<Order> {
        return new Promise<Order>((resolve, reject) => {
            axios({
                url: orders.get.url,
                method: orders.get.method,
                params: {
                    id: orderId
                }
            })
                .then(response => {
                    const order = new Order(response.data as unknown);
                    resolve(order);
                })
                .catch(e => reject(e));
        });
    }

    fulfillOrder(orderId: string): Promise<void> {
        return new Promise((resolve, reject) => {
            axios({
                url: orders.fulfill.url,
                method: orders.fulfill.method,
                data: {
                    id: orderId
                }
            })
                .then(() => resolve())
                .catch(e => reject(e));
        });
    }
}