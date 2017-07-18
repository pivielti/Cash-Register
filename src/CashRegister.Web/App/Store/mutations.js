export default {
    startLoading(state) {
        state.loading = true;
    },
    stopLoading(state) {
        state.loading = false;
    },
    alert(state, params) {
        state.snackbar = params;
    }
};