<template>
    <div>
        <h1>{{title}}</h1>
        <form novalidate>
            <md-input-container v-bind:class="{'md-input-invalid': errors.has('category')}">
                <label>Name</label>
                <md-input v-model="category.name" placeholder="Red" v-validate="{rules: {required: true}}" data-vv-name="category"></md-input>
                <span v-show="errors.has('category')" class="md-error">{{ errors.first('category') }}</span>
            </md-input-container>
            <md-input-container v-bind:class="{'md-input-invalid': errors.has('color')}" :style="{ color: category.color }">
                <label>Color</label>
                <md-input v-model="category.color" v-validate="{ rules: { required: true, regex: /#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$/} }" placeholder="#FF0000" data-vv-name="color"></md-input>
                <span v-show="errors.has('color')" class="md-error">{{ errors.first('color') }}</span>
            </md-input-container>
            <md-button class="md-raised md-primary margin-left-0" @click.native.prevent="save()">Save</md-button>
            <md-button class="md-primary" @click.native.prevent="cancel()">Cancel</md-button>
        </form>
    </div>
</template>

<script>
    module.exports = {
        props: ["id"],
        created() {
            this.fetch();
        },
        data() {
            return {
                category: {}
            }
        },
        computed: {
            title() {
                switch (this.$route.meta.mode) {
                    case "CREATE":
                        return "Create a category";
                    case "EDIT":
                        return "Edit a category";
                    default:
                        return "ERROR: You should set an edit mode!";
                }
            }
        },
        methods: {
            fetch() {
                if (this.$route.meta.mode != "EDIT")
                    return;

                this.$store.commit('startLoading');

                this.$http.get("/api/categories/" + this.id)
                    .then(response => {
                        this.category = response.body;
                    }, response => {
                        console.log(response);
                    }).then(() => {
                        this.$store.commit('stopLoading');
                    });
            },
            save() {
                this.$validator.validateAll().then(isValid => {
                    if (!isValid)
                        return;

                    this.$store.commit('startLoading');

                    if (this.$route.meta.mode == 'CREATE') {
                        this.$http.post("/api/categories/", this.category)
                            .then(response => { this.saveSuccess(response); }, response => { this.saveError(response); })
                            .then(() => { this.stopLoading(); });
                    }
                    if (this.$route.meta.mode == 'EDIT') {
                        this.$http.put("/api/categories/" + this.category.id, this.category)
                            .then(response => { this.saveSuccess(response); }, response => { this.saveError(response); })
                            .then(() => { this.stopLoading(); });
                    }
                }).catch((error) => {
                    alert(error);
                });
            },
            cancel() {
                this.$router.push({ name: 'categories-admin' });
            },
            saveSuccess(response) {
                this.$router.push({ name: 'categories-admin' });
            },
            saveError(response) {
                console.log(response.body);
            },
            stopLoading() {
                this.$store.commit('stopLoading');
            }
        }
    }
</script>
