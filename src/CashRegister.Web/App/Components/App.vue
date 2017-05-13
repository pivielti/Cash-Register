<template>
    <div id="app">
        <div class="row">
            <div class="columns large-12">
                <main-menu site-title="Cash Register"></main-menu>
            </div>
        </div>
        <div class="row">
            <div class="columns col-md-12">
                <transition name="fade">
                    <router-view></router-view>
                </transition>
            </div>
        </div>
    </div>
</template>

<script>
    import 'foundation-sites/dist/css/foundation.css';
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