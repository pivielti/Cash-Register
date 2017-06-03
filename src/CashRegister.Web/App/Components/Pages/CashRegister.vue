<template>
    <div id="letsgo">
        <div class="fit-to-screen flex-box">
            <div class="flex-box columns flex-1">
                <div class="flex-box m-r-5">
                    <!--@Html.Partial("_FiltersPartial")-->
                </div>
                <div class="flex-1 flex-box columns m-l-5 m-r-5">
                    <!--@Html.Partial("_ProductsPartial")-->
                </div>
                <div class="flex-box rows m-l-5">
                    <div class="alert alert-danger" id="total-price">
                        <h1 class="t-r m-reset">
                            <span>{{totalPrice}}</span>
                            <i class="glyphicon glyphicon-euro small fi-list"></i>
                        </h1>
                    </div>
                    <!--@Html.Partial("_DetailsPartial")-->
                </div>
            </div>
        </div>
        <hr />
        <!--@Html.Partial("_OperationsPartial")-->
    </div>
</template>

<script>
    import '../../Assets/Css/flex-grid.scss';

    module.exports = {
        data: function () {
            return {
                totalPrice: 0
            }
        },
        computed: {
        },
        components: {
        }
    }
</script>

<style lang="scss" scoped>
</style>