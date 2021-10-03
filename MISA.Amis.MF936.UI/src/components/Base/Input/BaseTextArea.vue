<template lang="">
    <div class="textarea-wrapper">
        <span v-if="label != ''" class="label">
			{{ label }} <span v-if="required" class="text-red">*</span></span
		>
        <textarea class="textarea" v-on="textAreaListeners" :placeholder="placeholder" rows="2"></textarea> 
    </div>
</template>
<script>
export default {
    name: 'BaseTextArea',
    props: {
        label: {
            type: String,
            default: '  '
        },
        required: {
            type: Boolean,
            default: false
        },
        placeholder: {
            type: String,
            default: ''
        }
    },
    computed: {
        /**
         * Lắng nghe sự kiện textArea
         * CreatedBy: NTDUNG (03/10/2021)
         */
        textAreaListeners() {
            return Object.assign({}, this.$listener, {
                input: (event) => {
                    this.$emit('input', event.target.value);
                }
            })
        }
    }
}
</script>
<style>
    @import url('../../../css/base/input/textarea.css'); 
</style>