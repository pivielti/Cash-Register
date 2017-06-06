<template>
    <md-table-row :key="category.id">
        <md-table-cell md-numeric>{{category.id}}</md-table-cell>
        <md-table-cell>{{category.name}}</md-table-cell>
        <md-table-cell>{{category.color}}</md-table-cell>
        <md-table-cell>
            <md-menu md-direction="bottom left">
                <md-button class="md-icon-button" md-menu-trigger>
                    <md-icon>more_horiz</md-icon>
                </md-button>
                <md-menu-content>
                    <md-menu-item>
                        <span>Edit</span><md-icon>edit</md-icon>
                    </md-menu-item>
                    <md-menu-item>
                        <span>Delete</span><md-icon>delete</md-icon>
                    </md-menu-item>
                </md-menu-content>
            </md-menu>
        </md-table-cell>
    </md-table-row>
</template>

<script>
    module.exports = {
        props: ["category"],
        data: function () {
            return {
            }   
        },
        computed: {
        },
        components: {
        }
    }
</script>

<style lang="scss" scoped>
</style>