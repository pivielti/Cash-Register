<template>
    <div>
        <!-- Categories table -->
        <md-table-card>
            <md-toolbar>
                <h1 class="md-title">Categories</h1>
                <md-button class="md-icon-button md-raised" @click.native="gotoCreatePage">
                    <md-icon>add</md-icon>
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
                    <category-table-row v-for="category in orderedCategories" :category="category" :key="category.id" @delete="openDeleteDialog(category)" @edit="gotoEditPage(category)"></category-table-row>
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
    import CategoryTableRow from './Categories/CategoryTableRow.vue';
    module.exports = {
        data() {
            return {
                selectedCategory: {},
                categories: []
            }
        },
        created() {
            this.fetchDatas();
        },
        computed: {
            orderedCategories() {
                return this.categories.sort(function (a, b) {
                    return a.name > b.name;
                });
            }
        },
        methods: {
            fetchDatas() {
                this.$store.commit('startLoading');
                this.$http.get('/api/categories').then(response => {
                    this.categories = response.body;
                }, response => {
                    console.log(response.body);
                    // error callback
                }).then(() => {
                    this.$store.commit('stopLoading');
                });
            },
            gotoCreatePage() {
                this.$router.push({ name: 'categories-admin-create', })
            },
            gotoEditPage(category) {
                this.$router.push({ name: 'categories-admin-edit', params: { id: category.id } });
            },
            openDeleteDialog(category) {
                this.selectedCategory = category;
                this.$refs["deleteDialog"].open();
            },
            deleteDialogClosed(state) {
                if (state == "cancel")
                    return;

                this.$http.delete('/api/categories/' + this.selectedCategory.id).then(response => {
                    var index = this.categories.indexOf(this.selectedCategory);
                    this.categories.splice(index, 1);
                }, response => {
                    console.log(response.body);
                });
            }
        },
        components: {
            CategoryTableRow: CategoryTableRow
        }
    }
</script>
