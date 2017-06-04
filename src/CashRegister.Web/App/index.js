import Vue from 'vue';
import VueRouter from 'vue-router';
import VueResource from 'vue-resource';
import Vuex from 'vuex';
import App from './Components/App.vue';
import Auth from '@websanova/vue-auth';
import AuthBearer from '@websanova/vue-auth/drivers/auth/bearer.js';
import AuthResource from '@websanova/vue-auth/drivers/http/vue-resource.1.x.js';
import AuthRouter from '@websanova/vue-auth/drivers/router/vue-router.2.x.js';

// register plugins
Vue.use(VueRouter);
Vue.use(VueResource);
Vue.use(Vuex);

// configure routes
import { routes } from './routes';
var router = new VueRouter({
    routes, /*linkActiveClass: 'active',*/
    linkExactActiveClass: 'active'
});
Vue.router = router;

// configure authentication
Vue.use(Auth, {
    auth: AuthBearer,
    http: AuthResource,
    router: AuthRouter,
    rolesVar: 'roles',
    parseUserData: function (data) {
        return data;
    }
});

new Vue({
    el: '#app',
    router: router,
    render: function (createElement) {
        return createElement(App);
    }
});
