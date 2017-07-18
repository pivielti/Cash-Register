<template>
    <div>
        <h1>{{title}}</h1>
        <form novalidate>
            <md-input-container v-bind:class="{'md-input-invalid': errors.has('name')}">
                <label>Name</label>
                <md-input v-model="product.name" v-validate="{ rules: { required: true } }" data-vv-name="name"></md-input>
                <span v-show="errors.has('name')" class="md-error">{{ errors.first('name') }}</span>
            </md-input-container>
            <md-input-container v-bind:class="{'md-input-invalid': errors.has('selling-price')}">
                <label>Selling price</label>
                <md-input v-model="product.price" v-validate="{ rules: { required: true, regex: /^[0-9]*(\.[0-9]{1,2})?$/ } }" data-vv-name="selling-price"></md-input>
                <span v-show="errors.has('selling-price')" class="md-error">{{ errors.first('selling-price') }}</span>
            </md-input-container>
            <md-input-container v-bind:class="{'md-input-invalid': errors.has('cost-price')}">
                <label>Cost price</label>
                <md-input v-model="product.costPrice" v-validate="{ rules: { required: true, regex: /^[0-9]*(\.[0-9]{1,2})?$/ } }" data-vv-name="cost-price"></md-input>
                <span v-show="errors.has('cost-price')" class="md-error">{{ errors.first('cost-price') }}</span>
            </md-input-container>
            <md-input-container v-bind:class="{'md-input-invalid': errors.has('category-id')}">
                <label>Category</label>
                <md-select v-model="product.categoryId" v-validate="{ rules: { required: true } }" data-vv-name="category-id">
                    <md-option v-for="cat in categories" :value="cat.id" :key="cat.id">{{cat.name}}</md-option>
                </md-select>
                <span v-show="errors.has('category-id')" class="md-error">{{ errors.first('category-id') }}</span>
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
            this.fetchCategories();
        },
        data() {
            return {
                categories: [],
                product: {}
            }
        },
        computed: {
            title() {
                switch (this.$route.meta.mode) {
                    case "CREATE":
                        return "Create a product";
                    case "EDIT":
                        return "Edit a product";
                    default:
                        return "ERROR: You should set an edit mode!";
                }
            }
        },
        methods: {
            fetchCategories() {
                this.$store.commit('startLoading');

                this.$http.get('/api/categories')
                    .then(response => {
                        this.categories = response.body;
                    }, response => {
                        alert(JSON.stringify(response));
                    }).then(() => {
                        this.$store.commit('stopLoading');
                    });
            },
            fetch() {
                if (this.$route.meta.mode != "EDIT")
                    return;

                this.$store.commit('startLoading');

                this.$http.get("/api/products/" + this.id)
                    .then(response => {
                        this.product = response.body;
                    }, response => {
                        console.log(response);
                    }).then(() => {
                        this.$store.commit('stopLoading');
                    });
            },
            save() {
                this.$validator.validateAll().then(isValid => {
                    if (!isValid && this.errors.length > 0) {
                        alert(JSON.stringify(this.errors));
                        return;
                    }

                    this.$store.commit('startLoading');

                    if (this.$route.meta.mode == 'CREATE') {
                        this.$http.post("/api/products/", this.product)
                            .then(response => { this.saveSuccess(response); }, response => { this.saveError(response); })
                            .then(() => { this.stopLoading(); });
                    }
                    if (this.$route.meta.mode == 'EDIT') {
                        this.$http.put("/api/products/" + this.id, this.product)
                            .then(response => { this.saveSuccess(response); }, response => { this.saveError(response); })
                            .then(() => { this.stopLoading(); });
                    }
                }).catch((error) => {
                    alert(error);
                });
            },
            cancel() {
                this.$router.push({ name: 'products-admin' });
            },
            saveSuccess(response) {
                this.$router.push({ name: 'products-admin' });
            },
            saveError(response) {
                this.$store.dispatch("alert", { level: "ERROR", message: response.body });
            },
            stopLoading() {
                this.$store.commit('stopLoading');
            }
        }
    }
</script>
