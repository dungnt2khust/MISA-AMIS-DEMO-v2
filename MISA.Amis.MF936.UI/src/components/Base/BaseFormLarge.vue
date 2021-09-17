<template lang="">
    <div v-show="formLargeState" class="formlarge">
        <div class="formlarge__header"></div>
        <div class="formlarge__body"></div>  
        <div class="formlarge__footer fx-space-between">
            <button @click="$emit('input', false)" class="button button--dark"> Huỷ</button> 
            <div class="formlarge__store fx">
                <button class="button button--dark mr-8"> Cất</button>
                <div class="button button--green"> Cất là In</div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: 'BaseFormLarge',
    props: {
        formLargeState: {
            type: Boolean,
            default: false
        }
    } 
}
</script>
<style>
    @import url('../../css/base/formlarge.css'); 
</style>