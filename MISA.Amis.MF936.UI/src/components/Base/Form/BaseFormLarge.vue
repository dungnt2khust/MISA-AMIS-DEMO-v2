<template lang="">
    <div v-show="formLargeState" class="formlarge">
        <div class="formlarge__header fx-space-between">
            <div class="fx-center">
                <base-history class="mr-16"/>
                <div class="formlarge__title"> {{ title}}</div> 
                <slot name="header"></slot>
            </div> 
            <div class="form__function fx-center-ver">
                <base-tutorial class="mr-16"/>
                <base-setting class="mr-10"/>
                <base-help/>
                <base-cancel/>
            </div>
        </div>
        <div class="formlarge__body">
            <slot name="body"></slot> 
        </div>  
        <div class="formlarge__footer fx-space-between">
            <slot name="footer"></slot>
        </div>
    </div>
</template>
<script>
// COMPONENTS
import BaseTutorial from '../BaseTutorial.vue'
import BaseHelp from './components/BaseHelp.vue'
import BaseCancel from './components/BaseCancel.vue'
import BaseSetting from './components/BaseSetting.vue'
import BaseHistory from './components/BaseHistory.vue'


export default {
    name: 'BaseFormLarge',
    components: {
        BaseTutorial,
        BaseHelp,
        BaseCancel,
        BaseSetting,
        BaseHistory,
    },
    props: {
        formLargeState: {
            type: Boolean,
            default: false
        },
        title: {
            type: String,
            default: ''
        }
    },
    methods: {
        /**
         * Ẩn form
         * CreatedBy: NTDUNG (23/09/2021)
         */
        hideForm() {
            this.$emit('input', false);
        }
    }
}
</script>
<style>
    @import url('../../../css/base/form/formlarge.css'); 
</style>