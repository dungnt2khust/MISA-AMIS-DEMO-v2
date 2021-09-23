<template lang="">
    <div class="button-double">
        <div
            @click="method()"
            class="button button--green button--half-left"
        >
            {{ label }}
        </div>
        <div
            class="button button--green button--half-right"
        ></div>
    </div>
</template>
<script>
    export default {
        name: 'BaseButtonDouble',
        props: {
            label: {
                type: String,
                default: ''
            },
            method: {
                type: Function,
                default: () => {}
            }
        }
    }
</script>
<style>
    @import url('../../../css/base/button/buttondouble.css');  
</style>