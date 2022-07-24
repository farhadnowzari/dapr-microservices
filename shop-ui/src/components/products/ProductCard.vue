<template>
    <v-card>
        <v-card-title>
            {{ product.name || 'Unknown product' }}
            <v-spacer></v-spacer>
            {{ `${product.unitPrice}$` }}
        </v-card-title>
        <v-divider></v-divider>
        <v-card-actions class="pa-3">
        <v-btn @click="createOrder()" outlined class="ml-auto" color="primary">
            <v-icon small class="mr-2">mdi-cart-outline</v-icon> Buy
        </v-btn>
        </v-card-actions>
    </v-card>
</template>

<script lang="ts">
import OrdersController from '@/services/controllers/OrdersController';
import { Product } from '@shop/domain/dist';
import { Vue, Component, Prop } from 'vue-property-decorator';

@Component
export default class ProductCard extends Vue {
    @Prop(Object)
    readonly product!: Product

    createOrder(): void {
        new OrdersController()
            .createOrder(this.product.id as string)
            .then(orderId => {
                this.$router.push({ name: "orders", params: { id: orderId.toString() } })
            });
    }
}
</script>