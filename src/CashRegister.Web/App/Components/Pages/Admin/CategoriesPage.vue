<template>
    <md-table-card>
        <md-toolbar>
            <h1 class="md-title">Categories</h1>
            <md-button class="md-icon-button" @click="refreshDatas">
                <md-icon>refresh</md-icon>
            </md-button>
        </md-toolbar>
        <md-table>
            <md-table-header>
                <md-table-row>
                    <md-table-head md-numeric>Id</md-table-head>
                    <md-table-head>Name</md-table-head>
                    <md-table-head>Color</md-table-head>
                    <md-table-head>
                    </md-table-head>
                </md-table-row>
            </md-table-header>
            <md-table-body>
                <category-table-row v-for="category in categories" :category="category"></category-table-row>
            </md-table-body>
        </md-table>
    </md-table-card>
</template>

<script>
    import CategoryTableRow from './Categories/CategoryTableRow.vue';
    module.exports = {
        data() {
            return {
                categories: [
                    { id: 0, name: 'Category 1', color: '#FF00FF' },
                    { id: 1, name: 'Category 2', color: '#80FF00' }
                ]
            }
        },
        methods: {
            refreshDatas() {
                alert('TODO: refresh datas');
            },
            addCategory() {

            },
            removeCategory() {

            }
        },
        components: {
            CategoryTableRow: CategoryTableRow
        }
    }
</script>

<style lang="scss" scoped>
</style>