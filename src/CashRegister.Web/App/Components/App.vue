<template>
    <div id="app">
        <md-layout md-row>
            <md-layout md-column>
                <main-menu site-title="Cash Register"></main-menu>
                <md-progress md-indeterminate v-if="$store.state.loading" class=""></md-progress>
            </md-layout>
        </md-layout>
        <md-layout md-align="center" md-align-small="center">
            <md-layout md-flex="80" md-flex-small="90">
                <transition name="fade">
                    <router-view></router-view>
                </transition>
            </md-layout>
        </md-layout>
    </div>
</template>

<script>
    import 'vue-material/dist/vue-material.css';
    import '../Assets/Css/site.scss';
    import MainMenu from './Menus/MainMenu.vue';

    module.exports = {
        data: function () {
            return {
            }
        },
        computed: {
        },
        components: {
            MainMenu: MainMenu,
        }
    }
</script>

<style lang="scss" scoped>
    .fade-enter-active,
    .fade-leave-active {
        transition-property: opacity;
        transition-duration: .25s;
    }

    .fade-enter-active {
        transition-delay: .25s;
    }

    .fade-enter,
    .fade-leave-active {
        opacity: 0
    }
</style>