<template>
    <div>
        <md-table-card>
            <md-toolbar>
                <h1 class="md-title">Products</h1>
                <md-button class="md-icon-button md-raised">
                    <md-icon>add</md-icon>
                </md-button>
            </md-toolbar>
            <md-table>
                <md-table-header>
                    <md-table-row>
                        <md-table-head md-numeric>Id</md-table-head>
                        <md-table-head>Name</md-table-head>
                        <md-table-head>Price</md-table-head>
                        <md-table-head>Cost Price</md-table-head>
                        <md-table-head>Category</md-table-head>
                        <md-table-head></md-table-head>
                    </md-table-row>
                </md-table-header>
                <md-table-body>
                    <product-table-row v-for="product in products" :product="product" :key="product.id" @delete="deleteProduct"></product-table-row>
                </md-table-body>
            </md-table>
        </md-table-card>
        <!-- Dialogs -->
        <md-dialog-confirm md-title="Confirm"
                           md-content-html="Do you really want to delete this item ?"
                           md-ok-text="Yes"
                           md-cancel-text="No"
                           ref="deleteDialog">
        </md-dialog-confirm>
    </div>
</template>

<script>
    import ProductTableRow from './Products/ProductTableRow.vue';
    module.exports = {
        data() {
            return {
                products: []
            }
        },
        created() {
            this.fetchDatas();
        },
        methods: {
            fetchDatas() {
                this.$http.get('/api/products').then(response => {
                    this.products = response.body;
                }, response => {
                    // error callback
                });
            },
            addCategory() {
            },
            deleteCategory() {
                this.$refs["deleteDialog"].open();
            }
        },
        components: {
            ProductTableRow: ProductTableRow
        }
    }
</script>

<style lang="scss" scoped>
</style>