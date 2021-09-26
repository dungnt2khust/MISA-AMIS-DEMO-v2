<template lang="">
    <div class="textarea-wrapper">
        <span v-if="label != ''" class="label">
			{{ label }} <span v-if="required" class="text-red">*</span></span
		>
        <textarea class="textarea" rows="2"></textarea> 
    </div>
</template>
<script>
export default {
    name: 'BaseTextArea',
    props: {
        label: {
            type: String,
            default: ''
        },
        required: {
            type: Boolean,
            default: false
        }
    }
}
</script>
<style>
    @import url('../../../css/base/input/textarea.css'); 
</style>