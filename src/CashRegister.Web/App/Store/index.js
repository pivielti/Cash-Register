import Vue from 'vue';
import Vuex from 'vuex';
import actions from './actions';
import mutations from './mutations';
import getters from './getters';

Vue.use(Vuex);

var store = new Vuex.Store({
    state: {
        loading: false,
        snackbar: {
            level: "ERROR",
            message: ""
        },
    },
    getters: getters,
    actions: actions,
    mutations: mutations
});

export default store;