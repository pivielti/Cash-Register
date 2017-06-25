<template>
    <div class="phone-viewport">
        <md-toolbar>
            <md-button class="md-icon-button" @click.native="toggleLeftSidenav">
                <md-icon>menu</md-icon>
            </md-button>
            <h2 class="md-title">{{siteTitle}}</h2>
        </md-toolbar>
        <md-sidenav class="md-left" ref="leftSidenav">
            <md-toolbar class="md-small">
                <div class="md-toolbar-container">
                    <h3 class="md-title">Main Menu</h3>
                </div>
            </md-toolbar>
            <md-list>
                <md-list-item>
                    <router-link :to="{ name: 'index' }">
                        <md-icon>home</md-icon><span>Home</span>
                    </router-link>
                </md-list-item>
                <md-list-item>
                    <router-link :to="{ name: 'cash-register' }">
                        <md-icon>attach_money</md-icon><span>Cash</span>
                    </router-link>
                </md-list-item>
                <md-divider class="md-inset"></md-divider>
                <md-list-item v-if="$auth.check()">
                    <router-link :to="{ name: 'admin-zone' }">
                        <md-icon>dashboard</md-icon><span>Admin</span>
                    </router-link>
                </md-list-item>
                <md-list-item v-if="$auth.check()">
                    <router-link to="nothing" @click.native="$auth.logout()">
                        <md-icon>exit_to_app</md-icon><span>Logout</span>
                    </router-link>
                </md-list-item>
                <md-list-item v-if="!$auth.check()">
                    <router-link :to="{ name: 'login' }">
                        <md-icon>account_circle</md-icon><span>Login</span>
                    </router-link>
                </md-list-item>
            </md-list>
        </md-sidenav>
    </div>
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
                //opened: false
            }
        },
        methods: {
            toggleLeftSidenav() {
                this.$refs.leftSidenav.toggle();
            }
        },
        watch: {
            $route() {
                if (this.$refs.leftSidenav.mdVisible)
                    this.$refs.leftSidenav.close();
            }
        }
    }
</script>

<style lang="scss" scoped>
    .main-menu {
        position: fixed;
        top: 0;
        left: 0;
        .button

    {
        .icon

    {
        border: 2px solid black;
        height: 30px;
        width: 30px;
    }

    }
    }
</style>