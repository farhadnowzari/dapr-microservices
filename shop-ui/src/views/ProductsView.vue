<template>
    <v-container class="mx-auto">
        <v-row>
            <v-col cols=12 sm="6" md="4" lg="3" :key="index" v-for="(product, index) in products">
                <ProductCard :product="product"></ProductCard>
            </v-col>
        </v-row>
    </v-container>
</template>

<script lang="ts">
import ProductsController from '@/services/controllers/ProductsController';
import { Product } from '@shop/domain/dist';
import { Vue, Component } from 'vue-property-decorator';
import ProductCard from '@/components/products/ProductCard.vue';

@Component({
    components: {
        ProductCard
    }
})
export default class ProductsView extends Vue {
    products: Product[] = [];


    initProducts(): void {
        new ProductsController()
            .getList()
            .then(products => {
                this.products = products.filter(x => x.quantity > 0);
            });
    }


    mounted(): void {
        this.initProducts();
    }
}

</script>