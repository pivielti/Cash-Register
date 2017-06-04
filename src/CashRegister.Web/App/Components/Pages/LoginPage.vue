<template>
    <div>
        <h1>Login page</h1>
        <input type="text" v-model="login" />
        <input type="text" v-model="pswd" />
        <button v-on:click="loginMe" class="button">Login</button>
    </div>
</template>

<script>
    module.exports = {
        data: function () {
            return {
                login: "TEST",
                pswd: "TEST123"
            }
        },
        methods: {
            loginMe: function () {
                var formData = new FormData();
                formData.append('username', this.login);
                formData.append('password', this.pswd);

                this.$auth.login({
                    contentType: 'application/x-www-form-urlencoded',
                    body: formData,
                    success: function () {
                        console.log('login success');
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
