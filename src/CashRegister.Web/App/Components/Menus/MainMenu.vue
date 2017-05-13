<template>
    <ul class="menu">
        <li class="menu-text">{{siteTitle}}</li>
        <router-link tag="li" :to="{ name: 'index' }"><a>Home</a></router-link>
        <router-link tag="li" :to="{ name: 'cash-register' }"><a>Cash Register</a></router-link>
        <!--<li><router-link :to="{ name: 'index' }">Home</router-link></li>
        <li><router-link :to="{ name: 'cash-register' }">Cash Register</router-link></li>-->
    </ul>
</template>

<script>
    module.exports = {
        props: {
            siteTitle: {
                type: String,
                required: true
            }
        },
        data: function () {
            return {
            }
        },
        computed: {
        }
    }
</script>

<style lang="scss" scoped>
    .menu {
        border: 1px solid blue;
    }
</style>