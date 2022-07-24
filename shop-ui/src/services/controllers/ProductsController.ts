import { Product } from "@shop/domain/dist";
import products from "../endpoints/products";

export default class ProductsController {
    getList(): Promise<Product[]> {
        return new Promise<Product[]>((resolve, reject) => {
            axios({
                url: products.list.url,
                method: products.list.method
            })
                .then(response => {
                    let products = response.data as Product[];
                    products = products.map(p => new Product(p));
                    resolve(products);
                }).catch((e) => reject(e));
        });
    }
}