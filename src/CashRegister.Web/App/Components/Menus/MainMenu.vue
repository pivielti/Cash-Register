<template>
    <ul class="menu">
        <li class="menu-text">{{siteTitle}}</li>
        <router-link tag="li" :to="{ name: 'index' }"><a>Home</a></router-link>
        <router-link tag="li" :to="{ name: 'cash-register' }"><a>Cash Register</a></router-link>
        <!-- Admin / Logout -->
        <router-link tag="li" :to="{ name: 'admin-zone' }" v-if="$auth.check()"><a>Admin</a></router-link>
        <li v-if="$auth.check()"><a v-on:click="$auth.logout()">Logout</a></li>
        <!-- Login / Register -->
        <router-link tag="li" :to="{ name: 'login' }" v-if="!$auth.check()"><a>Login</a></router-link>
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