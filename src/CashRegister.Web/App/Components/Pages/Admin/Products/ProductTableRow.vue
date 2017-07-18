<template>
    <md-table-row>
        <md-table-cell md-numeric>{{product.id}}</md-table-cell>
        <md-table-cell>{{product.name}}</md-table-cell>
        <md-table-cell><span>{{product.price}}</span></md-table-cell>
        <md-table-cell><span>{{product.costPrice}}</span></md-table-cell>
        <md-table-cell>{{product.category.name}}</md-table-cell>
        <md-table-cell>
            <md-menu md-direction="bottom left">
                <md-button class="md-icon-button" md-menu-trigger>
                    <md-icon>more_horiz</md-icon>
                </md-button>
                <md-menu-content>
                    <md-menu-item @click.native="$emit('edit')">
                        <span>Edit</span><md-icon>edit</md-icon>
                    </md-menu-item>
                    <md-menu-item @click.native="$emit('delete')">
                        <span>Delete</span><md-icon>delete</md-icon>
                    </md-menu-item>
                </md-menu-content>
            </md-menu>
        </md-table-cell>
    </md-table-row>
</template>

<script>
    module.exports = {
        props: ["product"]
    }
</script>