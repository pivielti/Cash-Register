import Vue from 'vue';
import VueRouter from 'vue-router';
import VueResource from 'vue-resource';
import Vuex from 'vuex'
import App from './Components/App.vue';

// register plugins
Vue.use(VueRouter);
Vue.use(VueResource);
Vue.use(Vuex);

// configure routes
import { routes } from './routes';

new Vue({
    el: '#app',
    router: new VueRouter({ routes, /*linkActiveClass: 'active',*/ linkExactActiveClass: 'active' }),
    render: function (createElement) {
        return createElement(App);
    }
})
