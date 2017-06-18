<template>
    <div>
        <md-table-card>
            <md-toolbar>
                <h1 class="md-title">Products</h1>
                <md-button class="md-icon-button" @click.native="fetchDatas">
                    <md-icon>refresh</md-icon>
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
        <md-dialog-confirm md-title="Confirmation"
                           md-content-html="Voulez-vous vraiment supprimer cet élément ?"
                           md-ok-text="Oui"
                           md-cancel-text="Non"
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