<template>
    <div>
        <md-table-card>
            <md-toolbar>
                <h1 class="md-title">Products</h1>
                <md-button class="md-icon-button md-raised" @click.native="gotoCreatePage">
                    <md-icon>add</md-icon>
                </md-button>
            </md-toolbar>
            <md-table>
                <md-table-header>
                    <md-table-row>
                        <md-table-head md-numeric>Id</md-table-head>
                        <md-table-head>Name</md-table-head>
                        <md-table-head>Selling Price</md-table-head>
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
        <!-- Delete Dialog -->
        <md-dialog-confirm md-title="Confirmation"
                           md-content-html="Voulez-vous vraiment supprimer cet élément ?"
                           md-ok-text="Oui"
                           md-cancel-text="Non"
                           @close="deleteDialogClosed"
                           ref="deleteDialog">
        </md-dialog-confirm>
    </div>
</template>

<script>
    import ProductTableRow from './Products/ProductTableRow.vue';
    module.exports = {
        data() {
            return {
                products: [],
                selectedProduct: null
            }
        },
        created() {
            this.fetchDatas();
        },
        methods: {
            fetchDatas() {
                this.$store.commit('startLoading');
                this.$http.get('/api/products').then(response => {
                    this.products = response.body;
                }, response => {
                    console.log(response.body);
                }).then(() => {
                    this.$store.commit('stopLoading');
                });
            },
            gotoCreatePage() {
                this.$router.push({ name: 'products-admin-create', })
            },
            gotoEditPage(product) {
                this.$router.push({ name: 'products-admin-edit', params: { id: product.id } });
            },
            openDeleteDialog(product) {
                this.selectedProduct = product;
                this.$refs["deleteDialog"].open();
            },
            deleteDialogClosed(state) {
                if (state == "cancel")
                    return;

                this.$http.delete('/api/products/' + this.selectedProduct.id).then(response => {
                    var index = this.products.indexOf(this.selectedProduct);
                    this.products.splice(index, 1);
                }, response => {
                    console.log(response.body);
                });
            }
        },
        components: {
            ProductTableRow: ProductTableRow
        }
    }
</script>

<style lang="scss" scoped>
</style>