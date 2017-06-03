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
                    url: '/api/token/create',
                    body: formData,
                    contentType: 'application/x-www-form-urlencoded',
                    success: function () {
                        console.log('success');
                    },
                    error: function () {
                        console.log('error');
                    },
                    rememberMe: true,
                    redirect: '/',
                    fetchUser: false
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
