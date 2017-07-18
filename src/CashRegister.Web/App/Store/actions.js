export default {
    alert({ commit }, level, message) {
        commit('alert', level, message);
    }
}