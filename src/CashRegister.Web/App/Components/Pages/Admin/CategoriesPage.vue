<template>
    <div>
        <md-table-card>
            <md-toolbar>
                <h1 class="md-title">Categories</h1>
                <md-button class="md-icon-button" @click.native="fetchDatas">
                    <md-icon>refresh</md-icon>
                </md-button>
            </md-toolbar>
            <md-table>
                <md-table-header>
                    <md-table-row>
                        <md-table-head md-numeric>Id</md-table-head>
                        <md-table-head>Name</md-table-head>
                        <md-table-head>Color</md-table-head>
                        <md-table-head></md-table-head>
                    </md-table-row>
                </md-table-header>
                <md-table-body>
                    <category-table-row v-for="category in categories" :category="category" :key="category.id" @delete="deleteCategory"></category-table-row>
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
    import CategoryTableRow from './Categories/CategoryTableRow.vue';
    module.exports = {
        data() {
            return {
                categories: []
            }
        },
        created() {
            this.fetchDatas();
        },
        methods: {
            fetchDatas() {
                this.$http.get('/api/categories').then(response => {
                    console.log(response.body);
                    this.categories = response.body;
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
            CategoryTableRow: CategoryTableRow
        }
    }
</script>

<style lang="scss" scoped>
</style>