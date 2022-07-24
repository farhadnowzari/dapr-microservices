import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
  {
    path: '/products',
    name: 'products',
    component: () => import('@/views/ProductsView.vue')
  },
  {
    path: '/orders/:id',
    name: 'orders',
    component: () => import('@/views/OrderView.vue'),
    props: true
  }
]

const router = new VueRouter({
  routes
})

export default router
