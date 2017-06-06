<template>
    <md-layout md-column>
        <div>
            <h1 class="md-display-3">Administration dashboard</h1>
        </div>
        <md-layout md-gutter md-row>
            <md-layout md-column md-flex="25">
                <md-list>
                    <md-list-item>
                        <router-link :to="{ name: 'admin-zone' }">Index</router-link>
                    </md-list-item>
                    <md-list-item>
                        <router-link :to="{ name: 'categories-admin' }">Categories</router-link>
                    </md-list-item>
                    <md-list-item>
                        <router-link :to="{ name: 'products-admin' }">Products</router-link>
                    </md-list-item>
                </md-list>
            </md-layout>
            <md-layout md-column>
                <router-view></router-view>
            </md-layout>
        </md-layout>
    </md-layout>
</template>

<script>
    module.exports = {
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