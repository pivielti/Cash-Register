<template>
    <md-dialog ref="editDialog" :md-click-outside-to-close="false" :md-esc-to-close="false">
        <md-dialog-title>{{dialogTitle}}</md-dialog-title>
        <md-dialog-content>
            <form>
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
            </form>
        </md-dialog-content>
        <md-dialog-actions>
            <md-button class="md-primary" @click.native="cancelEditDialog()">Cancel</md-button>
            <md-button class="md-primary" @click.native="saveEditDialog()">Save</md-button>
        </md-dialog-actions>
    </md-dialog>
</template>

<script>
    module.exports = {
        props: ["editMode", "categoryToEdit", "open"],
        data() {
            return {
                category: this.categoryToEdit
            }
        },
        computed: {
            dialogTitle() {
                switch (this.editMode) {
                    case "CREATE":
                        return "Create a new category";
                    case "EDIT":
                        return "Edit a category";
                    default:
                        return "ERROR: You should set an edit mode!";
                }
            }
        },
        methods: {
            dialogClosed() {
            },
            cancelEditDialog() {
                this.$refs["editDialog"].close();
                this.$emit("closed", "CANCEL");
            },
            saveEditDialog() {
                if (this.editMode == 'CREATE') {
                    this.$http.post("/api/categories", this.category)
                        .then(response => {
                            this.$refs["editDialog"].close();
                            this.$emit("closed", "SAVE", response.body);
                        }, response => {
                            console.log(response);
                        });
                }
                if (this.editMode == 'EDIT') {
                    alert("TODO");
                }
            }
        },
        watch: {
            categoryToEdit(category) {
                this.category = category;
            },
            open(open) {
                if (open) {
                    this.$refs["editDialog"].open();
                }
            }
        },
        components: {
        }
    }
</script>
