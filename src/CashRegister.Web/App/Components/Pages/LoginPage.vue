﻿<template>
    <form @submit.prevent="loginMe">
        <h1 class="md-display-3">Login page</h1>
        <md-input-container>
            <label>Login</label>
            <md-input type="text" required v-model="login"></md-input>
        </md-input-container>
        <md-input-container md-has-password>
            <label>Password</label>
            <md-input type="password" required v-model="pswd"></md-input>
        </md-input-container>
        <md-button type="submit" class="md-raised md-primary margin-left-0">Login</md-button>
    </form>
</template>

<script>
    module.exports = {
        data: function () {
            return {
                login: "administrator",
                pswd: "password"
            }
        },
        methods: {
            loginMe: function () {
                var formDatas = new FormData();
                formDatas.append("username", this.login);
                formDatas.append("password", this.pswd);
                this.$auth.login({
                    body: formDatas,
                    success: function () {
                    },
                    error: function () {
                        console.log('login error');
                    },
                    rememberMe: true,
                    redirect: '/',
                });
            }
        },
        computed: {
        },
        components: {
        }
    }
</script>

<!--<style lang="scss" scoped>
</style>-->
