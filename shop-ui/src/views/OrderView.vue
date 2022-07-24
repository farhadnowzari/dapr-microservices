<template>
    <v-container>
        <v-card :loading="order === null">
            <v-card-text v-if="order">
                <v-row>
                    <v-col cols="8">
                        <span class="text-h5">{{ order.product.name || "Unknown product" }}</span>
                    </v-col>
                    <v-divider vertical></v-divider>
                    <v-col class="d-flex flex-column" cols="4">
                        <span class="text-h5">Order info</span>
                        <div class="d-flex align-center text-body-1 mt-5">
                            <span>Unit Price:</span>
                            <span class="font-weight-bold ml-auto">{{ `${order.unitPrice}$` }}</span>
                        </div>
                        <div class="d-flex align-center text-body-1 mt-5">
                            <span>Quantity:</span>
                            <span class="font-weight-bold ml-auto">{{ `${order.quantity}` }}</span>
                        </div>
                        <v-divider></v-divider>
                        <div class="d-flex align-center text-body-1 mt-5">
                            <span>Price:</span>
                            <span class="font-weight-bold ml-auto">{{ `${totalPrice}$` }}</span>
                        </div>
                        <v-btn @click="checkout()" color="primary" class="mt-6" block>
                            Checkout
                        </v-btn>
                    </v-col>
                </v-row>
            </v-card-text>
        </v-card>
    </v-container>
</template>


<script lang="ts">
import OrdersController from '@/services/controllers/OrdersController';
import { Order } from '@shop/domain/dist';
import { Vue, Component, Prop } from 'vue-property-decorator';


@Component
export default class OrderView extends Vue {
    @Prop(String)
    readonly id!: string;

    order: Order | null = null;

    mounted(): void {
        new OrdersController()
            .getOrder(this.id)
            .then(order => {
                this.order = order;
            })
    }

    get totalPrice(): number {
        if(!this.order) return 0;
        const price = this.order.unitPrice * this.order.quantity;
        return price;
    }

    checkout(): void {
        if(!this.order) return;
        new OrdersController()
            .fulfillOrder(this.order.id as string)
            .then(() => {
                alert("Check the payment service!");
            })
        
    }
}
</script>